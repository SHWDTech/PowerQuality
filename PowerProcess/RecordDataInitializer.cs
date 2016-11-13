using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using MySql.Data.MySqlClient;
using PowerQualityModel.DataModel;
using PowerQualityModel.ViewModel;
using Repository;
using SHWDTech.Platform.Utility;

namespace PowerProcess
{
    public class RecordDataInitializer : ProcessBase
    {
        public static readonly Dictionary<Guid, string> Stage = new Dictionary<Guid, string>();

        public void InitialRecordData(RecordParams recordParams, Guid stageId)
        {
            Stage.Add(stageId, RecordProcessStage.OnCreatingRecord);
            var recordRepo = Repo<PowerRepository<Record>>();
            var newRecord = new Record
            {
                RecordName = recordParams.RecordName,
                RecordStartDateTime = DateTime.Parse(recordParams.RecordConfigs["StartDateTime"]),
                RecordDuration = TimeSpan.Parse(recordParams.RecordConfigs["RecordDuration"]),
                RecordEndDateTime = DateTime.Parse(recordParams.RecordConfigs["EndDateTime"])
            };
            recordRepo.AddOrUpdateDoCommit(newRecord);
            newRecord.ModelState = ModelState.UnChanged;

            var configRepo = Repo<PowerRepository<SystemConfig>>();
            var configs = configRepo.GetModelList(obj => obj.ConfigType == "RecordInitial")
                .ToDictionary(obj => obj.ConfigName, item => item.ConfigValue);

            var initialzation = $"{newRecord.Id}\r\n{recordParams.RecordConfigs["Period"]}\r\n{recordParams.RecordConfigs["Frequency"]}\r\n{recordParams.RecordConfigs["LineType"]}";

            foreach (var file in Directory.GetFiles(configs["dataDirectory"]))
            {
                if(!recordParams.FileList.Contains(Path.GetFileNameWithoutExtension(file)))
                {
                    File.Delete($"{configs["dataDirectory"]}\\{file}");
                }
            }

            if (Directory.GetFiles(configs["dataDirectory"]).Length != recordParams.FileList.Count)
            {
                Stage[stageId] = RecordProcessStage.MissingFile;
                return;
            }

            File.WriteAllText(configs["InitialzationFile"], initialzation);

            if (!Globals.IsProcessRunning(configs["MainProcessName"]))
            {
                Process.Start(configs["MainProcessPath"]);
            }

            File.WriteAllText(configs["FinishFile"], "0");
            File.WriteAllText(configs["StartFile"], "1");

            Stage[stageId] = RecordProcessStage.OnCaclating;

            while (File.ReadAllText(configs["FinishFile"]).Substring(0, 1) != "1")
            {
                Thread.Sleep(1000);
            }

            using (var connection = new MySqlConnection(configs["MySqlConnString"]))
            {
                Stage[stageId] = RecordProcessStage.OnAfterCaclating;
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
                                Value = newRecord.Id
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
                        Stage[stageId] = RecordProcessStage.Failed;
                        return;
                    }
                }
                File.WriteAllText(configs["ActiveFilePath"], string.Empty);
                File.WriteAllText(configs["HarmonicFilePath"], string.Empty);
                File.WriteAllText(configs["StartFile"], "0");
            }
            Stage[stageId] = RecordProcessStage.ProcessCompleted;
            newRecord.Finalized = true;
            recordRepo.AddOrUpdateDoCommit(newRecord);
        }
    }
}