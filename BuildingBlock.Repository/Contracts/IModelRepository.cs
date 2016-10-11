﻿using ModelExecuter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelExecuter.Repository.Contracts
{
    public interface IModelRepository : IRepository<Model.Model>
    {
        Model.Model GetByCode(string code);
    }
}