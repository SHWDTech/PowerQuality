using System;
using System.Web.Mvc;
using PowerProcess;
using PowerQualityModel.ViewModel;

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

            var process = new RecordProcess();
            model.Records.AddRange(process.GetRecords(obj =>
            obj.RecordStartDateTime >= model.Selection.StartDate
            && obj.RecordStartDateTime <= model.Selection.EndDate));

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