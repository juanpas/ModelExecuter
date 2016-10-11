using ModelExecuter.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelExecuter.Model
{
    public class MetadataItemType
    {
        public MetadataItemType()
        {
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
