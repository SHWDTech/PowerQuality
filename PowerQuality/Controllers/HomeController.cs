using System;
using System.Linq;
using System.Web.Mvc;
using PowerQuality.Models.PowerAnalysis;
using PowerQualityModel;
using Repository;

namespace PowerQuality.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new RecordSelectList()
            {
                Selection = new RecordSelect
                {
                    StartDate = DateTime.Now.AddMonths(-1),
                    EndDate = DateTime.Now
                }
            };

            var repo = new PowerRepository<Record>();
            repo.InitEntitySet();

            model.Records.AddRange(repo.GetModels(obj =>
            obj.RecordStartDateTime >= model.Selection.StartDate
            && obj.RecordStartDateTime <= model.Selection.EndDate).ToList());

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(RecordSelect model)
        {

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