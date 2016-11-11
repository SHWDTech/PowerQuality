using System.Collections.Generic;
using System.Web.Http;
using PowerProcess;

namespace PowerQuality.Controllers
{
    public class RecordConfigController : ApiController
    {
        public Dictionary<string, string> Get()
            => new RecordProcess().GetRecordRequirements();
    }
}
