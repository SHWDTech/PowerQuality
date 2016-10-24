using System;
using System.Collections.Generic;
using System.Linq;
using PowerQualityModel;

namespace PowerQuality.Common
{
    public class RecordCache
    {
        private static readonly Dictionary<Guid, RecordValues> RecordValuesCache = new Dictionary<Guid, RecordValues>();

        public static bool Cached(Guid recordGuid)
            => RecordValuesCache.ContainsKey(recordGuid);

        public static double LoadPercetage(Guid recordGuid)
            => RecordValuesCache[recordGuid].Count/ RecordValuesCache[recordGuid].Values.Count;

        public static void PushValue(Guid recordGuid, List<ActiveValue> values)
        {
            if (RecordValuesCache.ContainsKey(recordGuid))
            {
                RecordValuesCache[recordGuid].Values.AddRange(values);
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
    }
}