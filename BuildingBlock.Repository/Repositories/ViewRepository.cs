using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ModelExecuter.Repository.Contracts;
using ModelExecuter.Model;

namespace ModelExecuter.Repository
{
    public class ViewRepository : EFRepository<Model.View>, IViewRepository
    {
        public ViewRepository(DbContext context) : base(context) { }

        public Model.View GetByCode(string code)
        {
            return DbSet.Where(p => p.Code == code).FirstOrDefault();
        }

    }
}
