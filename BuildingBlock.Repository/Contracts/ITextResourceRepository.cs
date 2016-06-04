using BuildingBlock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Repository.Contracts
{
    public interface ITextResourceRepository : IRepository<TextResource>
    {
        string Get(string type, Language language, int relatedId, string defaultValue = "");

        T LocalizeObject<T>(T localizableObject, Language language) where T : class;

    }
}
