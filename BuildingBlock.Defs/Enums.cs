using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Defs
{
    public static class Enums
    {
        public enum ParameterType
        {
            String = 1,
            Integer,
            Float,
            Boolean,
            DateTime
        }

        public enum FileType
        {
            Modelo = 1,
            Photo = 21
        }

        public enum FileSubType
        {
            ModeloFinanciero = 1
        }

        public enum FilePersistenceSystemType
        {
            Local = 1,
            AzureFile,
            AzureBlob
        }

    }
}
