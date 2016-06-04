using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlock.Model;

namespace BuildingBlock.Repository.Configuration
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            HasRequired(c => c.Category)
                .WithMany(i => i.Products)
                .HasForeignKey(a => a.CategoryId);

            HasRequired(c => c.Brand)
                .WithMany(i => i.Products)
                .HasForeignKey(a => a.BrandId);

            HasOptional(c => c.Provider)
                .WithMany(i => i.Products)
                .HasForeignKey(a => a.ProviderId);
        }

    }
}
