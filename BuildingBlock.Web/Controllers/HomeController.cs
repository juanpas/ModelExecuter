using ModelExecuter.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModelExecuter.Web.Controllers
{
    public class HomeController : BBController
    {
        public HomeController(IMainUow uow, Utils.Utils utils)
        {
            Uow = uow;
            Utils = utils;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}