using System;
using System.Web.Mvc;
using PowerQuality.Models.PowerAnalysis;

namespace PowerQuality.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new RecordSelect()
            {
                StartDate = DateTime.Now.AddMonths(-1),
                EndDate = DateTime.Now
            };

            return View(model);
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