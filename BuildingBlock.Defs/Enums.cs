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
            Photo = 1,
            Document = 2
        }

        public enum FileSubType
        {
            ProductPhoto = 1,
            ProductQuotationDocument = 21
        }

        public enum FilePersistenceSystemType
        {
            Local = 1,
            Azure
        }

    }
}
