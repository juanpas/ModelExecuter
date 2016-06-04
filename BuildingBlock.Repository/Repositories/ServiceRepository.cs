using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BuildingBlock.Repository.Contracts;
using BuildingBlock.Model;

namespace BuildingBlock.Repository
{
    public class ServiceRepository : EFRepository<Service>, IServiceRepository
    {
        public ServiceRepository(DbContext context) : base(context) { }

        public Service GetByName(string name)
        {
            return DbSet.Where(p => p.Name == name).FirstOrDefault();
        }

    }
}
