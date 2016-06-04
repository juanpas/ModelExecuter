using BuildingBlock.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Model
{
    public class ProductSetting
    {
        public ProductSetting()
        {
        }

        public int Id { get; set; }

        public int? CategoryId { get; set; }
        public ProductCategory Category { get; set; }

        public int? BrandId { get; set; }
        public ProductBrand Brand { get; set; }

        public int? CityId { get; set; }
        public City City { get; set; }

        public int AdminPropertyId { get; set; }

        public int? SellerId { get; set; }
        public ProductSeller Seller { get; set; }

        public string DiscountPercentage { get; set; }

        public string ProductDescription { get; set; }

    }
}
