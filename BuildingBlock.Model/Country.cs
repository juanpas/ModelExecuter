using BuildingBlock.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Model
{
    public class Country
    {
        public Country()
        {
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public Guid? ProductSalesManagerId { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
