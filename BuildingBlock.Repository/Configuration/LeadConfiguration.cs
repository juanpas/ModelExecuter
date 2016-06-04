using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlock.Model;

namespace BuildingBlock.Repository.Configuration
{
    public class LeadConfiguration : EntityTypeConfiguration<Lead>
    {
        public LeadConfiguration()
        {
            HasRequired(c => c.Category)
                .WithMany(i => i.Leads)
                .HasForeignKey(a => a.CategoryId);
        }

    }
}
