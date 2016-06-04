using static BuildingBlock.Defs.Enums;

namespace BuildingBlock.FilePersistence
{
    public static class PersitenceSistemFactory
    {
        public static IPersistenceSystem GetPersistenseSystem(FilePersistenceSystemType systemType, FileType fileType, FileSubType fileSubType, string folder)
        {
            IPersistenceSystem persistenceSystem;

            switch (systemType)
            {
                case FilePersistenceSystemType.Azure :
                    persistenceSystem = new AzurePersistenceSystem(fileType, fileSubType, folder);
                    break;

                case FilePersistenceSystemType.Local:
                default:
                    persistenceSystem = new LocalPersistenceSystem(fileType, fileSubType, folder);
                    break;
            }

            return persistenceSystem;
        }


    }

    public class FileUploadResult
    {
        public int Code { get; set; }
    }

}