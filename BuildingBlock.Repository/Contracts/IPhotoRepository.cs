﻿using ModelExecuter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelExecuter.Repository.Contracts
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        IQueryable<Photo> Get(int typeId, int relatedId);
        Photo Get(int typeId, int relatedId, int index);
    }
}
