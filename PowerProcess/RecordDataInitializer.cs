using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using MySql.Data.MySqlClient;
using PowerQualityModel.DataModel;
using Repository;
using SHWDTech.Platform.Utility;

namespace PowerProcess
{
    public class RecordDataInitializer : ProcessBase
    {
        public bool InitialRecordData(long recordId)
        {
            var configRepo = Repo<PowerRepository<SystemConfig>>();
            var configs = configRepo.GetModelList(obj => obj.ConfigType == "RecordInitial")
                .ToDictionary(obj => obj.ConfigName, item => item.ConfigValue);

            if (!Globals.IsProcessRunning(configs["MainProcessName"]))
            {
                Process.Start(configs["MainProcessPath"]);
            }

            File.WriteAllText(configs["FinishFile"], "0");
            File.WriteAllText(configs["StartFile"], "1");

            while (File.ReadAllText(configs["FinishFile"]).Substring(0, 1) != "1")
            {
                Thread.Sleep(1000);
            }

            using (var connection = new MySqlConnection(configs["MySqlConnString"]))
            {
                connection.Open();
                using (var transction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var loadCmd = connection.CreateCommand())
                        {
                            loadCmd.CommandType = CommandType.Text;
                            loadCmd.CommandText = $"LOAD DATA LOCAL INFILE '{configs["ActiveFilePath"]}' INTO TABLE activevalues";
                            loadCmd.ExecuteNonQuery();
                            loadCmd.CommandText = $"LOAD DATA LOCAL INFILE '{configs["HarmonicFilePath"]}' INTO TABLE harmonics";
                            loadCmd.ExecuteNonQuery();
                        }
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "calcvoltageseconds";
                            cmd.CommandTimeout = int.MaxValue;
                            cmd.Parameters.Add(new MySqlParameter()
                            {
                                DbType = DbType.Int64,
                                Direction = ParameterDirection.Input,
                                ParameterName = "relativeRecordId",
                                Value = recordId
                            });
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "calcvoltagethreeseconds";
                            cmd.ExecuteNonQuery();
                        }

                        transction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transction.Rollback();
                        File.WriteAllText(configs["ActiveFilePath"], string.Empty);
                        File.WriteAllText(configs["HarmonicFilePath"], string.Empty);
                        LogService.Instance.Error("数据库操作执行失败。", ex);
                        return false;
                    }
                }
                File.WriteAllText(configs["ActiveFilePath"], string.Empty);
                File.WriteAllText(configs["HarmonicFilePath"], string.Empty);
                File.WriteAllText(configs["StartFile"], "0");
            }
            return true;
        }
    }
}