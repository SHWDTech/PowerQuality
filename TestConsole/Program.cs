using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PowerQualityModel;
using Repository;

namespace TestConsole
{
    class Program
    {
        private static int Running { get; set; }

        static void Main(string[] args)
        {
            var context = new PowerDbContext();
            var record = new Record
            {
                Id = Guid.NewGuid(),
                RecordName = "TestOne",
                RecordDateTime = DateTime.Now,
                RecordStartDateTime = DateTime.Now
            };
            context.Records.Add(record);
            context.SaveChanges();

            var start = DateTime.Now;
            var indexs = new List<int>();
            for (var i = 0; i < 3456; i++)
            {
                indexs.Add(i * 100);
            }

            var completed = 0;
            Running = 0;

            Parallel.ForEach(indexs, (index) =>
            {
                AddRunning();
                var done = false;
                while (!done)
                {
                    try
                    {
                        using (var ctx = new PowerDbContext())
                        {
                            var endIdnex = index + 100;
                            var rd = new Random();
                            var stand = 220.0d;
                            for (var j = index; j < endIdnex; j++)
                            {
                                stand += rd.Next(10, 20) / 100.0;
                                var value = new ActiveValue
                                {
                                    Id = Guid.NewGuid(),
                                    RecordGuid = record.Id,
                                    RecordIndex = index,
                                    RecordTimeTicks = record.RecordDateTime.AddMilliseconds( 250 * j).Ticks,
                                    Voltage_AN = Math.Round(stand, 2),
                                    Voltage_BN = Math.Round(stand + 0.1, 2),
                                    Voltage_CN = Math.Round(stand - 0.1, 2),
                                    Voltage_NG = Math.Round(rd.Next(10, 20) / 100.0, 2)
                                };
                                ctx.ActiveValues.Add(value);
                                index++;
                            }

                            Console.WriteLine(
                                $"QuestStarted => StartIndex：{index}， {DateTime.Now:yyyy-MM-dd HH:mm:ss fff}。");
                            ctx.Configuration.AutoDetectChangesEnabled = false;
                            ctx.SaveChanges();
                            completed += 1;
                            ReduceRunning();
                            Console.WriteLine(
                                $"EndInsert=> StartIndex：{index} {DateTime.Now:yyyy-MM-dd HH:mm:ss fff}。Completed：{completed}/{3456}。Running：{GetRunning()}");
                            done = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"QuestRunFiled => {index}。\r\nException:\r\n{ex}");
                    }
                }
            });

            var end = DateTime.Now;
            Console.WriteLine($"Completed, Used:{(end - start).TotalSeconds}");
            Console.ReadKey();
        }

        static void AddRunning()
        {
            Running += 1;
        }

        static void ReduceRunning()
        {
            Running -= 1;
        }

        static int GetRunning()
        {
            return Running;
        }
    }
}
