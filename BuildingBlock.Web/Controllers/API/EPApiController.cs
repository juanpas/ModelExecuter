using BuildingBlock.Defs;
using BuildingBlock.Model;
using BuildingBlock.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace BuildingBlock.Web.Controllers
{
    public abstract class EPApiController : ApiController
    {
        protected IMainUow Uow { get; set; }
        protected Utils.Utils Utils { get; set; }

    }
}
