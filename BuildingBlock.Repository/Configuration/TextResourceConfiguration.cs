using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlock.Model;

namespace BuildingBlock.Repository.Configuration
{
    public class TextResourceConfiguration : EntityTypeConfiguration<TextResource>
    {
        public TextResourceConfiguration()
        {
            HasRequired(c => c.Language)
                .WithMany(i => i.TextResources)
                .HasForeignKey(a => a.LanguageId);
        }

    }
}
