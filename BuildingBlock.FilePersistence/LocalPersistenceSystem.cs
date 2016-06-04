using System;
using BuildingBlock.Defs;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BuildingBlock.FilePersistence
{
    internal class LocalPersistenceSystem : PersistenceSystem, IPersistenceSystem
    {
        public LocalPersistenceSystem(Enums.FileType fileType, Enums.FileSubType fileSubType, string folder) : base(fileType, fileSubType, folder)
        {
        }

        public MultipartFormDataStreamProvider MultipartFormProvider
        {
            get {
                return new CustomMultipartFormDataStreamProvider(FinalDestinationFolder, this);
            }
        }


        public string GetNextIndexedFileName(string fileName, bool createFolder)
        {
            string extension = Path.GetExtension(fileName);
            string result = "0001" + extension;

            string workingPath = FinalDestinationFolder;

            if (Directory.Exists(workingPath))
            {
                var directoryFiles = from file in Directory.GetFiles(workingPath)
                                     orderby file ascending
                                     select file;
                string lastFileName = directoryFiles.LastOrDefault();

                try
                {
                    int lastIndex = Convert.ToInt32(Path.GetFileNameWithoutExtension(lastFileName));

                    result = (lastIndex + 1).ToString().PadLeft(4, '0') + extension;
                }
                catch (Exception ex)
                {
                    //Do nothing
                }
            }
            else
            {
                if (createFolder)
                    Directory.CreateDirectory(workingPath);
            }

            return result;
        }


        public override FileUploadResult SaveFile(string fileName, Stream fileContent)
        {
            FileUploadResult result = new FileUploadResult()
            {
                Code = -1
            };

            if (fileContent != null)
            {
                using (Stream outputStream = File.OpenWrite(FinalDestinationFolder + Path.DirectorySeparatorChar + fileName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = fileContent.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }

                result.Code = 0;
            }

            return result;
        }


        public Stream GetFile(string fileName)
        {
            Stream result = null;

            string filepath = FinalDestinationFolder + Path.DirectorySeparatorChar + fileName;
            try
            {
                result = File.OpenRead(filepath);
            }
            catch { }

            return result;
        }



    }







    internal class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        private LocalPersistenceSystem persistenceSystem;

        public CustomMultipartFormDataStreamProvider(string rootPath, LocalPersistenceSystem persistenceSystem) : base(rootPath)
        {
            this.persistenceSystem = persistenceSystem;
        }

        public CustomMultipartFormDataStreamProvider(string rootPath, int bufferSize, LocalPersistenceSystem persistenceSystem) : base(rootPath, bufferSize)
        {
            this.persistenceSystem = persistenceSystem;
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            string originalName = headers.ContentDisposition.FileName.Trim('"').Replace("&", "and");
            var name = NextFileName(originalName);

            return name;
        }

        private string NextFileName(string fileName)
        {
            string result = persistenceSystem.GetNextIndexedFileName(fileName, true);

            return result;
        }
    }

}