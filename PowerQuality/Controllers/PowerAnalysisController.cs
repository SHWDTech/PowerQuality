using System.Linq;
using System.Web.Mvc;
using PowerQuality.Models.PowerAnalysis;
using PowerQualityModel;
using Repository;

namespace PowerQuality.Controllers
{
    public class PowerAnalysisController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RecordData(RecordDataRequest request)
        {
            using (var ctx = new PowerDbContext())
            {
                var data = ctx.Set<ActiveValue>().Where(obj => obj.RecordGuid == request.RecordGuid
                                                               && obj.RecordIndex >= request.StartIndex
                                                               && obj.RecordIndex < request.EndIndex).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}