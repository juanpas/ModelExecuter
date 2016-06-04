using BuildingBlock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Repository.Contracts
{
    public interface IProductQuotationActivityRepository : IRepository<ProductQuotationActivity>
    {
        IQueryable<ProductQuotationActivity> GetByQuotation(int quotationId);
        IQueryable<File> GetAttachments(int quotationId);
    }
}
