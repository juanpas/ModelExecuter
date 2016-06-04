using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlock.Model;

namespace BuildingBlock.Repository.Configuration
{
    public class ProductQuotationActivityConfiguration : EntityTypeConfiguration<ProductQuotationActivity>
    {
        public ProductQuotationActivityConfiguration()
        {
            HasRequired(c => c.Quotation)
                .WithMany(i => i.Activity)
                .HasForeignKey(a => a.QuotationId);

        }

    }
}
