using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelExecuter.Model;

namespace ModelExecuter.Repository.Configuration
{
    public class ParameterConfiguration : EntityTypeConfiguration<Parameter>
    {
        public ParameterConfiguration()
        {
            HasRequired(c => c.Category)
                .WithMany(i => i.Parameters)
                .HasForeignKey(a => a.CategoryId);
        }

    }
}
