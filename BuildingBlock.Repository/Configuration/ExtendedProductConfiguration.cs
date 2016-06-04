using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlock.Model;

namespace BuildingBlock.Repository.Configuration
{
    public class ExtendedProductConfiguration : EntityTypeConfiguration<ExtendedProduct>
    {
        public ExtendedProductConfiguration()
        {
            HasRequired(c => c.Product)
                .WithMany(i => i.ExtendedProducts)
                .HasForeignKey(a => a.ProductId);
        }

    }
}
