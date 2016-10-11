using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ModelExecuter.Repository.Contracts;
using ModelExecuter.Model;

namespace ModelExecuter.Repository
{
    public class MetadataItemRepository : EFRepository<MetadataItem>, IMetadataItemRepository
    {
        public MetadataItemRepository(DbContext context) : base(context) { }

        public MetadataItem GetByCode(string code)
        {
            return DbSet.Where(p => p.Code == code).FirstOrDefault();
        }

    }
}
