using BuildingBlock.Model;
using System.Linq;

namespace BuildingBlock.Repository.Contracts
{
    public interface IProductQuotationRepository : IRepository<ProductQuotation>
    {
        IQueryable<ProductQuotation> GetBySellerAndCity(int sellerId, int cityId);
        IQueryable<ProductQuotation> GetUnassignedByCity(int cityId);
        ProductQuotation GetByIdWithActivity(int id);
        IQueryable<ProductQuotation> GetRandomWithPrice(int numberOfItems);

    }
}
