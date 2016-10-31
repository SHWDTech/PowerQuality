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
        public void LoadRecord(Guid recordGuid)
        {
            if (RecordCache.Cached(recordGuid)) return;
            var repo = Repo<PowerRepository<ActiveValue>>();
            var recordCount = repo.GetCount(obj => obj.RecordGuid == recordGuid);
            RecordCache.AddRecord(recordGuid, recordCount);

            //repo.Database.CommandTimeout = 244000;
            var values = repo.GetModels(obj => obj.RecordGuid == recordGuid).ToList();
            RecordCache.PushValue(recordGuid, values);
        }

        public List<Record> GetRecords(Expression<Func<Record, bool>> exp)
        {
            var repo = Repo<PowerRepository<Record>>();

            return repo.GetModels(exp).ToList();
        }

        public RecordInfo GetRecordInfo(Guid recordGuid)
        {
            var info = new RecordInfo();
            var recordRepo = Repo<PowerRepository<Record>>();
            info.Record = recordRepo.GetModelById(recordGuid);
            var infoRepo = Repo<PowerRepository<RecordConfig>>();
            info.RecordConfig = infoRepo.GetModel(obj => obj.RecordGuid == recordGuid);
            var dataRepo = Repo<PowerRepository<ActiveValue>>();
            info.RecordDataCount = dataRepo.GetCount(obj => obj.RecordGuid == recordGuid);

            return info;
        }

        public List<Harmonic> LoadHarmonic(RequestRange range)
        {
            var repo = Repo<PowerRepository<Harmonic>>();
            return repo.GetModels(obj => obj.RecordIndex >= range.StartIndex).Take(range.RequestCount).ToList();
        }

        public List<ActiveValue> LoadActiveValues(RequestRange range)
        {
            var repo = Repo<PowerRepository<ActiveValue>>();
            return repo.GetModels(obj => obj.RecordIndex >= range.StartIndex).Take(range.RequestCount).ToList();
        }
    }
}
