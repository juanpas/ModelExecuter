﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Model
{
    public class ProductBrand
    {
        public ProductBrand()
        {
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<ProductSetting> ProductSettings { get; set; }

    }
}