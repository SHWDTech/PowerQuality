using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository;
using PowerQualityModel;
using SHWDTech.Platform.Utility;

namespace PowerProcess
{
    public class RecordProcess : ProcessBase
    {
        public void LoadRecord(Guid recordGuid)
        {
            if (RecordCache.Cached(recordGuid) || RecordCache.OnLoading(recordGuid)) return;
            var repo = Repo<PowerRepository<ActiveValue>>();
            repo.InitEntitySet();
            var recordCount = repo.GetCount(obj => obj.RecordGuid == recordGuid);
            RecordCache.AddRecord(recordGuid, recordCount);
            RecordCache.SetLoaingStatus(recordGuid, true);

            var recordIndexs = new List<int>();
            var current = 0;
            while (current < recordCount)
            {
                recordIndexs.Add(current);
                current += 200;
            }

            Parallel.ForEach(recordIndexs, (index) =>
            {
                var done = false;
                while (!done)
                {
                    try
                    {
                        var dbContext = new PowerDbContext();
                        var values =
                            dbContext.Set<ActiveValue>()
                                .Where(obj => obj.RecordIndex >= index && obj.RecordIndex < index + 200)
                                .ToList();
                        RecordCache.PushValue(recordGuid, values);
                    }
                    catch (Exception ex)
                    {
                        LogService.Instance.Error("读取记录数据失败。", ex);
                        var innerEx = ex.InnerException;
                        while(innerEx != null)
                        {
                            LogService.Instance.Error("异常详细信息。", innerEx);
                        }
                    }

                    done = true;
                }
            });

            RecordCache.SetLoaingStatus(recordGuid, false);
        }
    }
}
