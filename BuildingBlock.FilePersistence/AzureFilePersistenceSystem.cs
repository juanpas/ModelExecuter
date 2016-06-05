using System;
using BuildingBlock.Defs;
using System.IO;
using System.Net.Http;

using Microsoft.Azure; // Namespace for Azure Configuration Manager
using Microsoft.WindowsAzure.Storage; // Namespaces for Storage Client Library
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;

namespace BuildingBlock.FilePersistence
{
    internal class AzureFilePersistenceSystem : PersistenceSystem, IPersistenceSystem
    {
        private CloudFileDirectory fileDirectory = null;

        public MultipartFormDataStreamProvider MultipartFormProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public AzureFilePersistenceSystem(Enums.FileType fileType, Enums.FileSubType fileSubType, string folder) : base(fileType, fileSubType, folder)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create a CloudFileClient object for credentialed access to File storage.
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();

            // Get a reference to the file share we created previously.
            CloudFileShare share = null;
            switch (FileType)
            {
                case Enums.FileType.Photo:
                    share = fileClient.GetShareReference("photos");
                    break;
            }

            // Ensure that the share exists.
            if (share.Exists())
            {
                // Get a reference to the root directory for the share.
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();

                // Get a reference to the directory we created previously.
                fileDirectory = rootDir.GetDirectoryReference( ((int)FileSubType).ToString() );
                if (!fileDirectory.Exists())
                {
                    fileDirectory.Create();
                }

                fileDirectory = fileDirectory.GetDirectoryReference(Folder);
                if (!fileDirectory.Exists())
                {
                    fileDirectory.Create();
                }
            }

        }

        public override FileUploadResult SaveFile(string fileName, Stream fileContent)
        {
            FileUploadResult result = new FileUploadResult()
            {
                Code = -1
            };

            if (fileContent != null)
            {
                CloudFile file = fileDirectory.GetFileReference(fileName);


                byte[] array;
                using (var ms = new MemoryStream())
                {
                    fileContent.CopyTo(ms);
                    array = ms.ToArray();
                }

                file.Create(array.Length);

                file.UploadFromByteArray(array, 0, array.Length);

                result.Code = 0;
            }

            return result;
        }


        public string GetNextIndexedFileName(string fileName, bool createFolder)
        {
            string extension = Path.GetExtension(fileName);
            string result = "0001" + extension;

            bool fileDirectoryExists = fileDirectory.Exists();

            if(createFolder && !fileDirectoryExists)
            {
                fileDirectory.Create();
                fileDirectoryExists = true;
            }

            if (fileDirectoryExists)
            {
                var directoryFiles = fileDirectory.ListFilesAndDirectories();

                foreach (IListFileItem directoryFile in directoryFiles)
                {
                    string oneFile = directoryFile.Uri.ToString();
                    string oneFile1 = directoryFile.StorageUri.ToString();
                }

                /*string lastFileName = directoryFiles. .LastOrDefault();

                try
                {
                    int lastIndex = Convert.ToInt32(Path.GetFileNameWithoutExtension(lastFileName));

                    result = (lastIndex + 1).ToString().PadLeft(4, '0') + extension;
                }
                catch (Exception ex)
                {
                    //Do nothing
                }





                // Get a reference to the file we created previously.
                CloudFile file = fileDirectory.GetFileReference(fileName);

                // Ensure that the file exists.
                if (file.Exists())
                {
                }*/
            }

            return result;
        }

        public Stream GetFile(string fileName)
        {
            Stream result = new MemoryStream();

            // Ensure that the directory exists.
            if (fileDirectory.Exists())
            {
                // Get a reference to the file we created previously.
                CloudFile file = fileDirectory.GetFileReference(fileName);

                // Ensure that the file exists.
                if (file.Exists())
                {
                    // Get the contents of the file.
                    file.DownloadToStream(result);

                    //Necesario porque cuando vuelva a leerse el stream, lo retorna vacío
                    result.Position = 0;
                }
            }

            return result;
        }
    }
}
/*

public class AzureBlobStorageMultipartProvider : MultipartFormDataStreamProvider
{
    private CloudBlobContainer _container;
    public AzureBlobStorageMultipartProvider(CloudBlobContainer container)
        : base(Path.GetTempPath())
    {
        _container = container;
        Files = new List<FileDetails>();
    }

    public List<FileDetails> Files { get; set; }

    public override Task ExecutePostProcessingAsync()
    {
        // Upload the files to azure blob storage and remove them from local disk
        foreach (var fileData in this.FileData)
        {
            string fileName = Path.GetFileName(fileData.Headers.ContentDisposition.FileName.Trim('"'));

            // Retrieve reference to a blob
            CloudBlob blob = _container.GetBlobReference(fileName);
            blob.Properties.ContentType = fileData.Headers.ContentType.MediaType;
            blob.UploadFile(fileData.LocalFileName);
            File.Delete(fileData.LocalFileName);
            Files.Add(new FileDetails
            {
                ContentType = blob.Properties.ContentType,
                Name = blob.Name,
                Size = blob.Properties.Length,
                Location = blob.Uri.AbsoluteUri
            });
        }

        return base.ExecutePostProcessingAsync();
    }
}

*/