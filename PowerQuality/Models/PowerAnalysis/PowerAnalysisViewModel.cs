using System;

namespace PowerQuality.Models.PowerAnalysis
{
    public class RecordDataRequest
    {
        public Guid RecordGuid { get; set; }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }
    }
}