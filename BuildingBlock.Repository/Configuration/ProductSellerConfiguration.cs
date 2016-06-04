using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlock.Model;

namespace BuildingBlock.Repository.Configuration
{
    public class ProductSellerConfiguration : EntityTypeConfiguration<ProductSeller>
    {
        public ProductSellerConfiguration()
        {
            HasMany<City>(s => s.Cities)
                .WithMany(c => c.ProductSellers)
                .Map(cs =>
                {
                    cs.MapLeftKey("ProductSellerId");
                    cs.MapRightKey("CityId");
                    cs.ToTable("ProductSeller_City");
                });

        }

    }
}
