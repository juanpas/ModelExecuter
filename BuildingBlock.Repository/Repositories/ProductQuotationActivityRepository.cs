using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BuildingBlock.Repository.Contracts;
using BuildingBlock.Model;

namespace BuildingBlock.Repository
{
    public class ProductQuotationActivityRepository : EFRepository<ProductQuotationActivity>, IProductQuotationActivityRepository
    {
        public ProductQuotationActivityRepository(DbContext context) : base(context) { }

        public IQueryable<ProductQuotationActivity> GetByQuotation(int quotationId)
        {
            return DbSet.Where(p => p.QuotationId == quotationId);
        }

        public IQueryable<File> GetAttachments(int quotationId)
        {
            return DbSet.Where(p => p.QuotationId == quotationId).SelectMany(a => a.Attachments);
        }
    }
}
