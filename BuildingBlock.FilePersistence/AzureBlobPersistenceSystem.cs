using System;
using ModelExecuter.Defs;
using System.IO;
using System.Net.Http;

using Microsoft.Azure; // Namespace for Azure Configuration Manager
using Microsoft.WindowsAzure.Storage; // Namespaces for Storage Client Library
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;

namespace ModelExecuter.FilePersistence
{
    internal class AzureBlobPersistenceSystem : PersistenceSystem, IPersistenceSystem
    {
        private CloudBlobContainer blobContainer = null;

        public MultipartFormDataStreamProvider MultipartFormProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public AzureBlobPersistenceSystem(Enums.FileType fileType, Enums.FileSubType fileSubType, string folder) : base(fileType, fileSubType, folder)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create a CloudBlobClient object for credentialed access to storage.
            CloudBlobClient client = storageAccount.CreateCloudBlobClient();

            // Get a reference to the blob container.
            switch (FileType)
            {
                case Enums.FileType.Modelo:
                    blobContainer = client.GetContainerReference("modelos");
                    break;
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
                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);
                blockBlob.UploadFromStream(fileContent);

                result.Code = 0;
            }

            return result;
        }


        public string GetNextIndexedFileName(string fileName, bool createFolder)
        {
            throw new NotImplementedException();
        }

        public Stream GetFile(string fileName)
        {
            Stream result = new MemoryStream();

            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);

            if (blockBlob.Exists())
            {
                // Get the contents of the blob.
                blockBlob.DownloadToStream(result);

                //Necesario porque cuando vuelva a leerse el stream, lo retorna vacío
                result.Position = 0;
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