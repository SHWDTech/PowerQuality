using System;
using System.Web.Mvc;
using PowerProcess;
using PowerQualityModel.ViewModel;

namespace PowerQuality.Controllers
{
    public class PowerAnalysisController : Controller
    {
        public ActionResult Record()
        {
            if (string.IsNullOrWhiteSpace(Request["recordId"])) return Redirect("/");
            var id = new Guid(Request["recordId"]);

            var process = new RecordProcess();
            var model = process.GetRecordInfo(id);
            return View(model);
        }

        public ActionResult RecordHarmonic()
        {
            var range = new RequestRange()
            {
                StartIndex = int.Parse(Request["StartIndex"]),
                RequestCount = int.Parse(Request["RequestCount"])
            };
            var process = new RecordProcess();
            var harmonics = process.LoadHarmonic(range);
            var jsonResult = Json(new
            {
                recordData = harmonics
            }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult RecordData(RecordDataRequest request)
        {
            var range = new RequestRange()
            {
                StartIndex = int.Parse(Request["StartIndex"]),
                RequestCount = int.Parse(Request["RequestCount"])
            };
            var process = new RecordProcess();
            var activeValues = process.LoadActiveValues(range);
            var jsonResult = Json(new
            {
                recordData = activeValues
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