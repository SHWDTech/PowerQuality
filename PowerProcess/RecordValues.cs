﻿using System.Collections.Generic;
using PowerQualityModel.DataModel;

namespace PowerProcess
{
    public class RecordValues
    {
        public double Count { get; set; }

        public bool LoadCompleted { get; set; }

        public List<ActiveValue> Values { get; } = new List<ActiveValue>();
    }
}