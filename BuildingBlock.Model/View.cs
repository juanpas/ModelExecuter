using ModelExecuter.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelExecuter.Model
{
    public class View
    {
        public View()
        {
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //public ICollection<MetadataItem> Metadata { get; set; }

        public string JSON { get; set; }

    }
}
