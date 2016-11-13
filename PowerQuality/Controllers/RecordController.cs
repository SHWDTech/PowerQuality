using System;
using System.Web.Http;
using PowerProcess;
using PowerQualityModel.ViewModel;
using System.Threading.Tasks;

namespace PowerQuality.Controllers
{
    public class RecordController : ApiController
    {
        public Guid Post([FromBody]RecordParams recordParams)
        {
            var record = Guid.NewGuid();
            Task.Factory.StartNew(() =>
            {
                var initial = new RecordDataInitializer();
                initial.InitialRecordData(recordParams, record);
            });

            return record;
        }

        public string Get([FromUri] Guid record)
            => RecordDataInitializer.Stage.ContainsKey(record)
                ? RecordDataInitializer.Stage[record]
                : RecordProcessStage.NotProcessing;
    }
}
