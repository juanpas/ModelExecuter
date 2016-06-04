using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BuildingBlock.Repository.Contracts;
using BuildingBlock.Model;

namespace BuildingBlock.Repository
{
    public class PhotoRepository : EFRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(DbContext context) : base(context) { }

        public IQueryable<Photo> Get(int typeId, int relatedId)
        {
            return DbSet.Where(p => p.SubTypeId == typeId && p.RelatedId == relatedId);
        }

        public Photo Get(int typeId, int relatedId, int index)
        {
            return DbSet.Where(p => p.SubTypeId == typeId && p.RelatedId == relatedId && p.Index == index).FirstOrDefault();
        }

    }
}
