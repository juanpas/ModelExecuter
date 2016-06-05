using BuildingBlock.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildingBlock.Web.Controllers
{
    public class ModeloFinancieroController : BBController
    {
        public ModeloFinancieroController(IMainUow uow, Utils.Utils utils)
        {
            Uow = uow;
            Utils = utils;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}