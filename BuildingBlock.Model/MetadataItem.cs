using ModelExecuter.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelExecuter.Model
{
    public class MetadataItem
    {
        public MetadataItem()
        {
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Source { get; set; }
        public int MetadataItemTypeId { get; set; }
        public MetadataItemType MetadataItemType { get; set; }

    }
}
