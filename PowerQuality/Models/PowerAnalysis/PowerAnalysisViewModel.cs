using PowerQualityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PowerQuality.Models.PowerAnalysis
{
    public class RecordDataRequest
    {
        public Guid RecordGuid { get; set; }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }
    }

    public class RecordSelect
    {
        [Display(Name = "起始日期")]
        public DateTime StartDate { get; set; }

        [Display(Name = "结束日期")]
        public DateTime EndDate { get; set; }
    }

    public class RecordSelectList
    {
        public RecordSelect Selection { get; set; }

        public List<Record> Records { get; set; } = new List<Record>();
    }
}