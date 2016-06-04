﻿using BuildingBlock.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Model
{
    public class City
    {
        public City()
        {
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public Guid? ProductSalesManagerId { get; set; }

        public ICollection<ProductSetting> ProductSettings { get; set; }

        public ICollection<ProductSeller> ProductSellers { get; set; }

        public ICollection<ProductQuotation> ProductQuotations { get; set; }

    }
}