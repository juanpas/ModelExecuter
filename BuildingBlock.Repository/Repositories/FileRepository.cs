using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ModelExecuter.Repository.Contracts;
using ModelExecuter.Model;

namespace ModelExecuter.Repository
{
    public class FileRepository : EFRepository<File>, IFileRepository
    {
        public FileRepository(DbContext context) : base(context) { }

        public IQueryable<File> Get(int typeId, int subTypeId, int relatedId)
        {
            return DbSet.Where(p => p.TypeId == typeId && p.SubTypeId == subTypeId && p.RelatedId == relatedId);
        }

        public File Get(int typeId, int subTypeId, int relatedId, int index)
        {
            return DbSet.Where(p => p.TypeId == typeId && p.SubTypeId == subTypeId && p.RelatedId == relatedId && p.Index == index).FirstOrDefault();
        }

    }
}
