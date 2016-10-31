using System;
using System.Collections.Generic;
using System.Linq;
using PowerQualityModel;
using PowerQualityModel.DataModel;

namespace PowerProcess
{
    public class RecordCache
    {
        private static readonly Dictionary<Guid, RecordValues> RecordValuesCache = new Dictionary<Guid, RecordValues>();

        public static bool Cached(Guid recordGuid)
            => RecordValuesCache.ContainsKey(recordGuid) && RecordValuesCache[recordGuid].LoadCompleted;

        public static double LoadPercetage(Guid recordGuid)
        {
            if (!RecordValuesCache.ContainsKey(recordGuid)) return -1;
            return RecordValuesCache[recordGuid].Values.Count / RecordValuesCache[recordGuid].Count ;
        }

        public static void PushValue(Guid recordGuid, List<ActiveValue> values)
        {
            if (!RecordValuesCache.ContainsKey(recordGuid)) return;
            var recordValue = RecordValuesCache[recordGuid];
            recordValue.Values.AddRange(values);
            if (recordValue.Values.Count == Convert.ToInt32(recordValue.Count))
            {
                recordValue.LoadCompleted = true;
            }
        }

        public static void AddRecord(Guid recordGuid, int count)
        {
            if (RecordValuesCache.ContainsKey(recordGuid))
            {
                var recordValues = RecordValuesCache[recordGuid];
                recordValues.Values.Clear();
                recordValues.Count = count;
            }
            else
            {
                RecordValuesCache.Add(recordGuid, new RecordValues() {Count = count});
            }

            if (RecordValuesCache.Count > 3)
            {
                RecordValuesCache.Remove(RecordValuesCache.Last().Key);
            }
        }

        public static RecordValues GetRecord(Guid recordGuid)
            => RecordValuesCache[recordGuid];
    }
}