using System;
using BuildingBlock.Defs;
using static BuildingBlock.Defs.Enums;
using System.Net;
using System.IO;

namespace BuildingBlock.FilePersistence
{
    internal class PersistenceSystem
    {
        private FileSubType fileSubType;
        private FileType fileType;
        private string folder;

        public FileType FileType
        {
            get { return fileType; }
        }

        public FileSubType FileSubType
        {
            get { return fileSubType; }
        }

        public string Folder
        {
            get { return folder; }
        }

        public string ParentFolder
        {
            get {
                string parentFolder = AppDomain.CurrentDomain.BaseDirectory;

                switch (fileType)
                {
                    case FileType.Photo:
                        parentFolder += "Photos";
                        break;
                }

                return parentFolder;
            }
        }

        public string FinalDestinationFolder
        {
            get
            {
                string finalDestinationFolder = ParentFolder + Path.DirectorySeparatorChar + (int)fileSubType;

                if (!string.IsNullOrEmpty(folder))
                    finalDestinationFolder += Path.DirectorySeparatorChar + folder;

                return finalDestinationFolder;
            }
        }


        public PersistenceSystem(FileType fileType, FileSubType fileSubType, string folder)
        {
            this.fileType = fileType;
            this.fileSubType = fileSubType;
            this.folder = folder;
        }

        public FileUploadResult SaveFileFromURL(string sourceURL, string fileName)
        {
            Stream downloadedFile = Utils.DownloadRemoteImageFile(sourceURL);

            FileUploadResult result = SaveFile(fileName, downloadedFile);

            return result;
        }

        public virtual FileUploadResult SaveFile(string fileName, Stream downloadedFile)
        {
            throw new NotImplementedException();
        }
    }
}