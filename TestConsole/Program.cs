using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PowerQualityModel;
using Repository;
using SHWDTech.Platform.Utility;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var recordGuid = new Guid("884760cf-27b3-4d0a-9174-2ef8bee1c179");
            var context = new PowerDbContext();
            var startDate = DateTime.Now;
            var duration = TimeSpan.FromMilliseconds(345600*250);
            context.Set<Record>().Add(new Record
            {
                Id = recordGuid,
                RecordName = "测试记录",
                RecordDateTime = startDate,
                RecordStartDateTime = startDate,
                RecordDuration = duration,
                RecordEndDateTime = startDate + duration
            });
            context.SaveChanges();

            var recordCount = 345600;

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
                        var rd = new Random();
                        for (var i = index; i < index + 200; i++)
                        {
                            var avg = Math.Round(rd.Next(1, 100) / 100.0 + 220, 2);
                            dbContext.Set<ActiveValue>().Add(new ActiveValue
                            {
                                Id = Globals.NewCombId(),
                                Voltage_AN = avg,
                                Voltage_BN = avg + 0.2,
                                Voltage_CN = avg - 0.2,
                                Voltage_NG = avg - 220,
                                RecordGuid = recordGuid,
                                RecordIndex = i
                            });
                        }

                        dbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        LogService.Instance.Error("添加记录数据失败", ex);
                        Console.WriteLine(ex);
                    }

                    done = true;
                }
            });
        }
    }
}
