using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BuildingBlock.Repository.Contracts;
using BuildingBlock.Model;

namespace BuildingBlock.Repository
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context) { }

        public Product GetByName(string name)
        {
            return DbSet.Where(p => p.Name == name).FirstOrDefault();
        }

        public Product GetByCode(string code)
        {
            return DbSet.Where(p => p.Code == code).FirstOrDefault();
        }

        public Product GetByIdExtended(int id)
        {
            return DbSet.Include(p => p.ExtendedProducts).Where(p => p.Id == id).FirstOrDefault();
        }

        public IQueryable<Product> GetAllExtended()
        {
            return DbSet.Include(p => p.ExtendedProducts);
        }

        public IQueryable<Product> Search(int brandId, int categoryId, string name)
        {
            IQueryable<Product> result = DbSet;

            if(brandId > 0)
            {
                result = result.Where(p => p.BrandId == brandId);
            }
            if(categoryId > 0)
            {
                result = result.Where(p => p.CategoryId == categoryId);
            }
            if (!string.IsNullOrEmpty(name))
            {
                result = result.Where(p => p.Name.Contains(name));
            }

            return result;
        }

    }
}
