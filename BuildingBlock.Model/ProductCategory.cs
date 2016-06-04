using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Model
{
    [LocalizableObjectKeyProperty("Id")]
    public class ProductCategory
    {
        public ProductCategory()
        {
        }

        public int Id { get; set; }
        public string Code { get; set; }
        [LocalizationCode("PRODUCTCATEGORY_NAME")]
        public string Name { get; set; }
        [LocalizationCode("PRODUCTCATEGORY_DESCRIPTION")]
        public string Description { get; set; }

        public int? ParentCategoryId { get; set; }
        public ProductCategory ParentCategory { get; set; }


        public ICollection<Product> Products { get; set; }
        public ICollection<ProductSetting> ProductSettings { get; set; }

    }
}
