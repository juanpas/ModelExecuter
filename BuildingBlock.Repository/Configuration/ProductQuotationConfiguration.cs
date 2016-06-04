using System.Data.Entity.ModelConfiguration;
using BuildingBlock.Model;

namespace BuildingBlock.Repository.Configuration
{
    public class ProductQuotationConfiguration : EntityTypeConfiguration<ProductQuotation>
    {
        public ProductQuotationConfiguration()
        {
            HasRequired(c => c.Product)
                .WithMany(i => i.Quotations)
                .HasForeignKey(a => a.ProductId);

            HasRequired(c => c.City)
                .WithMany(i => i.ProductQuotations)
                .HasForeignKey(a => a.CityId);

            HasOptional(c => c.Seller)
                .WithMany(i => i.Quotations)
                .HasForeignKey(a => a.SellerId);
        }

    }
}
