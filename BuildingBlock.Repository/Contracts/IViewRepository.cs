using ModelExecuter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelExecuter.Repository.Contracts
{
    public interface IViewRepository : IRepository<Model.View>
    {
        Model.View GetByCode(string code);
    }
}
