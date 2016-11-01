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
                current += 100;
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
                        var activeValues = new List<ActiveValue>();
                        var harmonics = new List<Harmonic>();
                        for (var i = index; i < index + 100; i++)
                        {
                            var avg = Math.Round(rd.Next(1, 100) / 100.0 + 220, 2);
                            var cur = Math.Round(rd.Next(1, 100) / 100.0 + 120, 2);
                            var activeValue = new ActiveValue
                            {
                                Id = Globals.NewCombId(),
                                Voltage_AN = avg,
                                Voltage_BN = avg + 0.2,
                                Voltage_CN = avg - 0.2,
                                Voltage_NG = avg - 220,
                                Voltage_AB = avg + 0.3,
                                Voltage_BC = avg + 0.2,
                                Voltage_CA = avg + 0.1,
                                Current_A = cur,
                                Current_B = cur + 2,
                                Current_C = cur - 2,
                                Current_N = cur - 120,
                                RecordGuid = recordGuid,
                                RecordIndex = i,
                                RecordTimeTicks = startDate.AddMilliseconds(i * 250).Ticks
                            };
                            activeValues.Add(activeValue);
                            harmonics.Add(new Harmonic()
                            {
                                ActiveValueGuid = activeValue.Id,
                                Id = Globals.NewCombId(),
                                RecordGuid = recordGuid,
                                RecordIndex = i
                            });
                        }
                        dbContext.Configuration.AutoDetectChangesEnabled = false;
                        dbContext.Configuration.ValidateOnSaveEnabled = false;
                        dbContext.ActiveValues.AddRange(activeValues);
                        dbContext.Harmonics.AddRange(harmonics);
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
