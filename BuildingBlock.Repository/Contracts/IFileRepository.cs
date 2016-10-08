using ModelExecuter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelExecuter.Repository.Contracts
{
    public interface IFileRepository : IRepository<File>
    {
        IQueryable<File> Get(int typeId, int subTypeId, int relatedId);
        File Get(int typeId, int subTypeId, int relatedId, int index);
    }
}
