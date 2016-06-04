using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BuildingBlock.Repository.Contracts;
using BuildingBlock.Model;

namespace BuildingBlock.Repository
{
    public class ModuleRepository : EFRepository<Module>, IModuleRepository
    {
        public ModuleRepository(DbContext context) : base(context) { }

    }
}
