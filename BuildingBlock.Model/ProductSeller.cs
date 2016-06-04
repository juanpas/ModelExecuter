using BuildingBlock.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Model
{
    public class ProductSeller
    {
        public ProductSeller()
        {
        }

        public int Id { get; set; }
        public Guid ApplicationUserId { get; set; }

        public ICollection<City> Cities { get; set; }
        public ICollection<ProductQuotation> Quotations { get; set; }
        public ICollection<ProductSetting> ProductSettings { get; set; }

    }
}
