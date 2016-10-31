using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PowerQualityModel.DataModel;

namespace PowerQualityModel.ViewModel
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

    public class RecordInfo
    {
        public Record Record { get; set; }

        public RecordConfig RecordConfig { get; set; }

        public int RecordDataCount { get; set; }
    }

    public class RequestRange
    {
        public int StartIndex { get; set; }

        public int RequestCount { get; set; }
    }
}