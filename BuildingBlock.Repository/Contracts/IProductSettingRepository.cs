using BuildingBlock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Repository.Contracts
{
    public interface IProductSettingRepository : IRepository<ProductSetting>
    {
        void OptimizeProductSetting(ProductSetting productSetting);
    }
}
