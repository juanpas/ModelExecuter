using ModelExecuter.Defs;
using ModelExecuter.Model;
using ModelExecuter.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace ModelExecuter.Web.Controllers
{
    public abstract class EPApiController : ApiController
    {
        protected IMainUow Uow { get; set; }
        protected Utils.Utils Utils { get; set; }

    }
}
