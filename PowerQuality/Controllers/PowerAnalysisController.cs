using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PowerProcess;
using PowerQuality.Models.PowerAnalysis;

namespace PowerQuality.Controllers
{
    public class PowerAnalysisController : Controller
    {
        public ActionResult Record()
        {
            if (string.IsNullOrWhiteSpace(Request["recordId"])) return Redirect("/");
            var id = new Guid(Request["recordId"]);
            Task.Factory.StartNew(() =>
            {
                var process = new RecordProcess();
                process.LoadRecord(id);
            });

            ViewBag.recordId = id;
            return View();
        }

        public ActionResult RecordData(RecordDataRequest request)
        {
            var values = RecordCache.GetRecord(request.RecordGuid);
            var jsonResult = Json(new
            {
                data = values.Values.Where(obj => obj.RecordIndex >= request.StartIndex && obj.RecordIndex < request.EndIndex)
            }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult LoadPercentage()
        {
            if (string.IsNullOrWhiteSpace(Request["recordGuid"])) return null;

            var recordGuid = new Guid(Request["recordGuid"]);
            var percentage = RecordCache.LoadPercetage(recordGuid);

            return Json(new { percentage }, JsonRequestBehavior.AllowGet);
        }
    }
}