using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Repository;
using PowerQualityModel.DataModel;
using PowerQualityModel.ViewModel;

namespace PowerProcess
{
    public class RecordProcess : ProcessBase
    {
        public void LoadRecord(long recordGuid)
        {
            if (RecordCache.Cached(recordGuid)) return;
            var repo = Repo<PowerRepository<ActiveValue>>();
            var recordCount = repo.GetCount(obj => obj.RecordId == recordGuid);
            RecordCache.AddRecord(recordGuid, recordCount);

            //repo.Database.CommandTimeout = 244000;
            var values = repo.GetModels(obj => obj.RecordId == recordGuid).ToList();
            RecordCache.PushValue(recordGuid, values);
        }

        public List<Record> GetRecords(Expression<Func<Record, bool>> exp)
        {
            var repo = Repo<PowerRepository<Record>>();

            return repo.GetModels(exp).ToList();
        }

        public RecordInfo GetRecordInfo(long recordGuid)
        {
            var info = new RecordInfo();
            var recordRepo = Repo<PowerRepository<Record>>();
            info.Record = recordRepo.GetModelById(recordGuid);
            var infoRepo = Repo<PowerRepository<RecordConfig>>();
            info.RecordConfig = infoRepo.GetModel(obj => obj.RecordId == recordGuid);
            var dataRepo = Repo<PowerRepository<ActiveValue>>();
            info.RecordDataCount = dataRepo.GetCount(obj => obj.RecordId == recordGuid);

            return info;
        }

        public Dictionary<string, List<object>> LoadHarmonic(RequestRange range)
        {
            var repo = Repo<PowerRepository<Harmonic>>();
            repo.Database.CommandTimeout = 14400;
            var harmonics = repo.GetModelsInclude(obj => 
            obj.RecordIndex >= range.StartIndex 
            && obj.RecordIndex < range.StartIndex + range.RequestCount,
            new List<string>() { "Record" }).ToList();
            return typeof(Harmonic).GetProperties().Where(prop => prop.PropertyType == typeof(double) || prop.Name == "RecordTime")
                .ToDictionary(prop => prop.Name, prop => harmonics.Select(obj => obj.GetType().GetProperty(prop.Name).GetValue(obj, null)).ToList());
        }

        public Dictionary<string, List<object>> LoadActiveValues(RequestRange range)
        {
            var repo = Repo<PowerRepository<ActiveValue>>();
            repo.Database.CommandTimeout = 14400;
            var activeValues = repo.GetModelsInclude(obj => 
            obj.RecordId == range.RecordId
            && obj.RecordIndex >= range.StartIndex 
            && obj.RecordIndex < range.StartIndex + range.RequestCount,
            new List<string>() { "Record" }).ToList();
            return typeof(ActiveValue).GetProperties().Where(prop => prop.PropertyType == typeof(double) || prop.Name == "RecordTime")
                .ToDictionary(prop => prop.Name, prop => activeValues.Select(obj => obj.GetType().GetProperty(prop.Name).GetValue(obj, null)).ToList());
        }

        public Dictionary<string, List<object>> LoadVoltageCurrentSecond(RequestRange range)
        {
            var repo = Repo<PowerRepository<VoltageCurrentSecond>>();
            repo.Database.CommandTimeout = 14400;
            var activeValues = repo.GetModelsInclude(obj => 
            obj.RecordId == range.RecordId
            && obj.RecordIndex >= range.StartIndex 
            && obj.RecordIndex < range.StartIndex + range.RequestCount,
            new List<string>() { "Record" }).ToList();
            return typeof(VoltageCurrentSecond).GetProperties().Where(prop => prop.PropertyType == typeof(double) || prop.Name == "RecordTime")
                .ToDictionary(prop => prop.Name, prop => activeValues.Select(obj => obj.GetType().GetProperty(prop.Name).GetValue(obj, null)).ToList());
        }

        public Dictionary<string, List<object>> LoadVoltageCurrentThreeSecond(RequestRange range)
        {
            var repo = Repo<PowerRepository<VoltageCurrentThreeSeconds>>();
            repo.Database.CommandTimeout = 14400;
            var activeValues = repo.GetModelsInclude(obj => 
            obj.RecordId == range.RecordId
            && obj.RecordIndex >= range.StartIndex 
            && obj.RecordIndex < range.StartIndex + range.RequestCount,
            new List<string>() { "Record" }).ToList();
            return typeof(VoltageCurrentThreeSeconds).GetProperties().Where(prop => prop.PropertyType == typeof(double) || prop.Name == "RecordTime")
                .ToDictionary(prop => prop.Name, prop => activeValues.Select(obj => obj.GetType().GetProperty(prop.Name).GetValue(obj, null)).ToList());
        }
    }
}
