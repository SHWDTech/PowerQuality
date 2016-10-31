using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PowerQualityModel.DataModel;
using Repository;
using SHWDTech.Platform.Utility;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //PropTest();
            GenerateData();
        }

        static void GenerateData()
        {
            var recordGuid = new Guid("884760cf-27b3-4d0a-9174-2ef8bee1c179");
            var context = new PowerDbContext();
            var startDate = DateTime.Now;
            var duration = TimeSpan.FromMilliseconds(345600 * 250);
            context.Set<Record>().Add(new Record
            {
                Id = recordGuid,
                RecordName = "测试记录",
                RecordDateTime = startDate,
                RecordStartDateTime = startDate,
                RecordDuration = duration,
                RecordEndDateTime = startDate + duration
            });
            context.Set<RecordConfig>().Add(new RecordConfig
            {
                CalcPrecision = 250,
                Frequency = 256,
                LineType = LineType.StarWithMiddle,
                RecordGuid = recordGuid
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
                            var activeValue = new ActiveValue
                            {
                                Id = Globals.NewCombId(),
                                Voltage_AN = avg,
                                Voltage_BN = avg + 0.2,
                                Voltage_CN = avg - 0.2,
                                Voltage_NG = avg - 220,
                                RecordGuid = recordGuid,
                                RecordIndex = i,
                                RecordTimeTicks = startDate.AddMilliseconds(i * 250).Ticks
                            };
                            dbContext.Set<ActiveValue>().Add(activeValue);
                            dbContext.Set<Harmonic>().Add(new Harmonic()
                            {
                                ActiveValueGuid = activeValue.Id,
                                Id = Globals.NewCombId(),
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

        private static void PropTest()
        {
            var har = new List<Harmonic>();
            var i = 0;
            while (i < 1)
            {
                har.Add(new Harmonic());
                i++;
            }
            foreach (var source in typeof(Harmonic).GetProperties().Where(prop => Globals.IsPrimitive(prop.PropertyType)))
            {
                foreach (var harmonic in har)
                {
                    Console.WriteLine(source.GetValue(harmonic));
                }
            }

            Console.ReadKey();
        }
    }
}
