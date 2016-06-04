using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BuildingBlock.Repository.Contracts;
using BuildingBlock.Model;

namespace BuildingBlock.Repository
{
    public class CompanyRepository : EFRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext context) : base(context) { }

    }
}
