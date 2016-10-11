using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ModelExecuter.Repository.Contracts;
using ModelExecuter.Model;

namespace ModelExecuter.Repository
{
    public class ModelRepository : EFRepository<Model.Model>, IModelRepository
    {
        public ModelRepository(DbContext context) : base(context) { }

        public Model.Model GetByCode(string code)
        {
            return DbSet.Where(p => p.Code == code).FirstOrDefault();
        }

    }
}
