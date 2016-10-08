using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ModelExecuter.Repository.Contracts;
using ModelExecuter.Model;

namespace ModelExecuter.Repository
{
    public class CompanyRepository : EFRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext context) : base(context) { }

    }
}
