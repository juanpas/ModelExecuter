using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ModelExecuter.Repository.Contracts;
using ModelExecuter.Model;

namespace ModelExecuter.Repository
{
    public class ParameterRepository : EFRepository<Parameter>, IParameterRepository
    {
        public ParameterRepository(DbContext context) : base(context) { }

        public Parameter GetByName(string name)
        {
            return DbSet.Where(p => p.Name == name).FirstOrDefault();
        }

    }
}
