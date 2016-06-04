using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BuildingBlock.Repository.Contracts;
using BuildingBlock.Model;

namespace BuildingBlock.Repository
{
    public class ProductQuotationRepository : EFRepository<ProductQuotation>, IProductQuotationRepository
    {
        public ProductQuotationRepository(DbContext context) : base(context) { }

        public IQueryable<ProductQuotation> GetBySellerAndCity(int sellerId, int cityId)
        {
            return DbSet.Include(p => p.Product).Include(p => p.Seller)
                .Where(p => p.SellerId == sellerId && p.CityId == cityId)
                .OrderBy(p => p.RequestDate);
        }

        public IQueryable<ProductQuotation> GetUnassignedByCity(int cityId)
        {
            return DbSet.Include(p => p.Product).Include(p => p.Seller)
                .Where(p => p.SellerId == null && p.CityId == cityId)
                .OrderBy(p => p.RequestDate);
        }

        public override IQueryable<ProductQuotation> GetAll()
        {
            return DbSet.Include(p => p.Product).Include(p => p.Seller);
        }

        public ProductQuotation GetByIdWithActivity(int id)
        {
            return DbSet.Include(p => p.Product).Include(p => p.Seller)
                .Include(q => q.Activity).Include(q => q.City).Include(q => q.City.Country)
                .Where(p => p.Id == id).FirstOrDefault();
        }

        public IQueryable<ProductQuotation> GetRandomWithPrice(int numberOfItems)
        {
            return DbSet.Include(p => p.Product)
                .Where(p => p.Price > 0)
                .OrderBy(i => Guid.NewGuid())
                .Take(Math.Min(DbSet.Count(), numberOfItems));

        }

    }
}
