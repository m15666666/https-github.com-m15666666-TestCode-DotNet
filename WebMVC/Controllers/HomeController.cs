using Castle.Core.Logging;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ILogger Logger { get; set; }

        private readonly ISession session;

        public HomeController(ISession session)
        {
            this.session = session;
        }

        public ActionResult Index()
        {
            Logger.Warn(" HomeController Index");

            ViewBag.Message = "Your application description page.";
            ViewBag.EventCount = session.QueryOver<Event>().RowCount();

            return View();
        }

        public ActionResult About()
        {
            Logger.Warn(" HomeController About");

            return View();
        }

        public ActionResult Contact()
        {
            Logger.Warn(" HomeController Contact");

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}