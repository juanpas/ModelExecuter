using BuildingBlock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Repository.Contracts
{
    public interface IParameterRepository : IRepository<Parameter>
    {
        Parameter GetByName(string name);
    }
}
