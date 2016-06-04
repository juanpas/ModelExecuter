using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BuildingBlock.Repository.Contracts;
using BuildingBlock.Model;

namespace BuildingBlock.Repository
{
    public class LeadRepository : EFRepository<Lead>, ILeadRepository
    {
        public LeadRepository(DbContext context) : base(context) { }

        public Lead GetByName(string name)
        {
            return DbSet.Where(p => p.Name == name).FirstOrDefault();
        }

    }
}
