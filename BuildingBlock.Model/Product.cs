using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Model
{
    [LocalizableObjectKeyProperty("Id")]
    public class Product
    {
        public Product()
        {
        }

        public int Id { get; set; }
        public string Code { get; set; }
        [LocalizationCode("PRODUCT_NAME")]
        public string Name { get; set; }
        [LocalizationCode("PRODUCT_SHORTDESCRIPTION")]
        public string ShortDescription { get; set; }
        [LocalizationCode("PRODUCT_DESCRIPTION")]
        public string Description { get; set; }
        [LocalizationCode("PRODUCT_DETAILS")]
        public string Details { get; set; }
        [LocalizationCode("PRODUCT_TECHNICALINFO")]
        public string TechnicalInfo { get; set; }
        [LocalizationCode("PRODUCT_DELIVERYINFO")]
        public string DeliveryInfo { get; set; }
        [LocalizationCode("PRODUCT_KEYWORDS")]
        public string Keywords { get; set; }

        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }

        public int BrandId { get; set; }
        public ProductBrand Brand { get; set; }

        public int? ProviderId { get; set; }
        public ProductProvider Provider { get; set; }

        public string ProvidedProduct { get; set; }

        public float Price { get; set; }

        public ICollection<ExtendedProduct> ExtendedProducts { get; set; }

        public ICollection<ProductQuotation> Quotations { get; set; }

    }
}
