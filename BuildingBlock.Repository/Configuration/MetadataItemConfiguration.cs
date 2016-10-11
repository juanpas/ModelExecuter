using System.Data.Entity.ModelConfiguration;
using ModelExecuter.Model;

namespace ModelExecuter.Repository.Configuration
{
    public class MetadataItemConfiguration : EntityTypeConfiguration<MetadataItem>
    {
        public MetadataItemConfiguration()
        {
            HasRequired(c => c.MetadataItemType)
                .WithMany()
                .HasForeignKey(a => a.MetadataItemType);
        }

    }
}
