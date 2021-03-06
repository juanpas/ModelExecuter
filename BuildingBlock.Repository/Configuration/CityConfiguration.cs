﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelExecuter.Model;

namespace ModelExecuter.Repository.Configuration
{
    public class CityConfiguration : EntityTypeConfiguration<City>
    {
        public CityConfiguration()
        {
            HasRequired(c => c.Country)
                .WithMany(i => i.Cities)
                .HasForeignKey(a => a.CountryId);
        }

    }
}
