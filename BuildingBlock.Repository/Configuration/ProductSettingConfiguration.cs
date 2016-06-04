using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlock.Model;

namespace BuildingBlock.Repository.Configuration
{
    public class ProductSettingConfiguration : EntityTypeConfiguration<ProductSetting>
    {
        public ProductSettingConfiguration()
        {
            HasOptional(c => c.Category)
                .WithMany(i => i.ProductSettings)
                .HasForeignKey(a => a.CategoryId);

            HasOptional(c => c.Brand)
                .WithMany(i => i.ProductSettings)
                .HasForeignKey(a => a.BrandId);

            HasOptional(c => c.City)
                .WithMany(i => i.ProductSettings)
                .HasForeignKey(a => a.CityId);

            HasOptional(c => c.Seller)
                .WithMany(i => i.ProductSettings)
                .HasForeignKey(a => a.SellerId);
        }

    }
}
