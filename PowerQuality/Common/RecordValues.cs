using System.Collections.Generic;
using PowerQualityModel;

namespace PowerQuality.Common
{
    public class RecordValues
    {
        public double Count { get; set; }

        public List<ActiveValue> Values { get; } = new List<ActiveValue>();
    }
}