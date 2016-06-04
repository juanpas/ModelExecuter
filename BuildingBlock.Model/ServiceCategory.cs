using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Model
{
    public class ServiceCategory
    {
        public ServiceCategory()
        {

        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int ParentCategoryId { get; set; }
        public ServiceCategory ParentCategory { get; set; }


        public ICollection<Service> Services { get; set; }

    }
}
