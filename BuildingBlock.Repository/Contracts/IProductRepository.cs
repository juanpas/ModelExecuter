using BuildingBlock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Repository.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetByName(string name);
        Product GetByCode(string code);
        Product GetByIdExtended(int id);
        IQueryable<Product> GetAllExtended();
        IQueryable<Product> Search(int brandId, int categoryId, string name);

    }
}
