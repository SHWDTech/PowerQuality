using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PowerProcess;
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
            //GenerateData();
            var exe = new RecordDataInitializer();
            exe.InitialRecordData(1);
            //GenerateFileForMySql();
        }

        private static void GenerateData()
        {
            var context = new PowerDbContext();
            var startDate = DateTime.Now;
            var duration = TimeSpan.FromMilliseconds(345600 * 250);
            context.Set<Record>().Add(new Record
            {
                Id = 1,
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
                RecordId = 1
            });
            context.SaveChanges();

            //var recordCount = 345600;

            //var recordIndexs = new List<int>();
            //var current = 0;
            //while (current < recordCount)
            //{
            //    recordIndexs.Add(current);
            //    current += 100;
            //}

            //Parallel.ForEach(recordIndexs, (index) =>
            //{
            //    var done = false;
            //    while (!done)
            //    {
            //        try
            //        {
            //            var dbContext = new PowerDbContext();
            //            var rd = new Random();
            //            var activeValues = new List<ActiveValue>();
            //            var harmonics = new List<Harmonic>();
            //            for (var i = index; i < index + 100; i++)
            //            {
            //                var avg = Math.Round(rd.Next(1, 100) / 100.0 + 220, 2);
            //                var cur = Math.Round(rd.Next(1, 100) / 100.0 + 120, 2);
            //                var activeValue = new ActiveValue
            //                {
            //                    Voltage_AN = avg,
            //                    Voltage_BN = avg + 0.2,
            //                    Voltage_CN = avg - 0.2,
            //                    Voltage_NG = avg - 220,
            //                    Voltage_AB = avg + 0.3,
            //                    Voltage_BC = avg + 0.2,
            //                    Voltage_CA = avg + 0.1,
            //                    Current_A = cur,
            //                    Current_B = cur + 2,
            //                    Current_C = cur - 2,
            //                    Current_N = cur - 120,
            //                    RecordId = 1,
            //                    RecordIndex = i,
            //                    RecordTimeTicks = startDate.AddMilliseconds(i * 250).Ticks
            //                };
            //                activeValues.Add(activeValue);
            //                harmonics.Add(new Harmonic()
            //                {
            //                    RecordId = 1,
            //                    RecordIndex = i
            //                });
            //            }
            //            dbContext.Configuration.AutoDetectChangesEnabled = false;
            //            dbContext.Configuration.ValidateOnSaveEnabled = false;
            //            dbContext.ActiveValues.AddRange(activeValues);
            //            dbContext.Harmonics.AddRange(harmonics);
            //            dbContext.SaveChanges();
            //        }
            //        catch (Exception ex)
            //        {
            //            LogService.Instance.Fail("添加记录数据失败", ex);
            //            Console.WriteLine(ex);
            //        }

            //        done = true;
            //    }
            //});
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

        private static void GenerateFileForMySql()
        {
            var active = new ActiveValue
            {
                Id = 1,
                RecordId = 1,
                RecordIndex = 0,
                RecordTimeTicks = DateTime.Now.Ticks,
                Voltage_AN = 221.51,
                Voltage_BN = 222.15,
                Voltage_CN = 218.50,
                Voltage_NG = 1.25,
                Current_A = 13.56,
                Current_B = 15.64,
                Current_C = 16.44,
                Frequency = 50.125
            };
            var props = typeof(ActiveValue).GetProperties();
            using (var file = File.CreateText("d:\\power_activeValues.txt"))
            {
                for (var i = 0; i < 345600; i++)
                {
                    foreach (var t in props)
                    {
                        if ((!t.PropertyType.IsPrimitive && t.PropertyType != typeof(Guid))
                            || t.Name == "RecordTime"
                            || t.Name == "ModelState"
                            || t.Name == "IsNew") continue;
                        string value;
                        //Debug.Write($"{props[j].Name}\r\n");
                        if (t.PropertyType == typeof(bool))
                        {
                            value = (bool)t.GetValue(active) ? "1" : "0";
                        }
                        else
                        {
                            value = t.GetValue(active).ToString();
                        }
                        file.Write(value);
                        if (t.Name != "HasSurge")
                        {
                            file.Write("\t");
                        }
                    }
                    active.Id++;
                    active.RecordIndex++;
                    file.Write("\r\n");
                }
            }
        }
    }
}
