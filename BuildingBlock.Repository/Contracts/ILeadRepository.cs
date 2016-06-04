using BuildingBlock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Repository.Contracts
{
    public interface ILeadRepository : IRepository<Lead>
    {
        Lead GetByName(string name);
    }
}
