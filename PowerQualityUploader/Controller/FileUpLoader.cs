using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using PowerQualityModel.ViewModel;
using PowerQualityUploader.Model;

namespace PowerQualityUploader.Controller
{
    public class FileUpLoader
    {
        public static Dictionary<string, string> ConfigRequirements { get; private set; }

        public static Dictionary<string, string> ConfigDictionary { get; private set; }

        public static string ServerAddr => AppConfig.ServerAddr;

        private static int _fileCount;

        private static int _fileProcessed;

        public static int FileCount => _fileCount;

        private static bool TryParseConfig(string configString, out Dictionary<string, string> config)
        {
            try
            {
                var items = configString.Split(new[] { "\r\n" }, StringSplitOptions.None).Where(cfg => !string.IsNullOrWhiteSpace(cfg));
                config = items.Select(cfg => cfg.Split('=')).ToDictionary(values => values[0], values => values[1]);
            }
            catch (Exception)
            {
                config = null;
                return false;
            }
            return true;
        }

        public static void LoadRequirements()
        {
            if (!File.Exists($"{Directory.GetCurrentDirectory()}\\configs.json")) return;
            ConfigRequirements = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText($"{Directory.GetCurrentDirectory()}\\configs.json"));
        }

        public static void LoadDict()
        {
            ConfigDictionary =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(
                    File.ReadAllText($"{Directory.GetCurrentDirectory()}\\dict.json"));
        }

        public static Dictionary<Guid, Dictionary<string, string>> LoadRecord(string directory)
        {
            var directories = Directory.GetDirectories(directory);
            var records = new Dictionary<Guid, Dictionary<string, string>>();
            foreach (var dir in directories.Where(obj => File.Exists($"{obj}\\power.cfg")))
            {
                try
                {
                    var fileCount = Directory.GetFiles(dir, "*.HEX", SearchOption.AllDirectories).Length;
                    Dictionary<string, string> recordConfigs;
                    if (!TryParseConfig(File.ReadAllText($"{dir}\\power.cfg"), out recordConfigs)) continue;
                    var duration = GetDuration(recordConfigs, fileCount);
                    recordConfigs.Add("Duration", string.Format("{0:dd}d {0:hh}h {0:mm}m {0:ss}s {0:fff}ms", duration));
                    recordConfigs.Add("RecordDuration", $"{duration:G}");
                    recordConfigs.Add("EndDateTime",
                        $"{(DateTime.Parse(recordConfigs["StartDateTime"]) + duration):yyyy-MM-dd HH:mm:ss.fff}");
                    recordConfigs.Add("Directory", dir);
                    records.Add(Guid.NewGuid(), recordConfigs);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return records;
        }

        private static TimeSpan GetDuration(Dictionary<string, string> configs, int fileCount)
        {
            var interval = 20.0 / int.Parse(configs["SampleRate"]);
            return new TimeSpan(Convert.ToInt64(16385 * interval * 10000) * fileCount);
        }

        public static void UpdateConfigRequirements()
        {
            var postClient = new PostClient($"{ServerAddr}RecordConfig");
            var responseJosn = postClient.Get();
            ConfigRequirements = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseJosn);
            File.WriteAllText($"{Directory.GetCurrentDirectory()}\\configs.json", JsonConvert.SerializeObject(ConfigRequirements, Formatting.Indented));
        }

        public static void UploadRecordFiles(Dictionary<string, string> recordConfigs, View.Progress progress, int startLine, int endLine)
        {
            var currentFileIndex = startLine / 16384;
            var offset = (startLine % 16384);
            var files = Directory.GetFiles(recordConfigs["Directory"], "*.HEX", SearchOption.AllDirectories);
            var currentLine = startLine;
            var fileList = new List<Dictionary<string, int>>();
            var fileName = 0;
            while (currentLine < endLine)
            {
                fileList.Add(new Dictionary<string, int>
                {
                    { "file", currentFileIndex },
                    { "offset", offset },
                    { "fileName",  fileName}
                });
                fileName++;
                offset = 0;
                currentLine += 16384;
            }
            _fileCount = (endLine - startLine) / 16384;
            _fileProcessed = 0;
            var fileNameList = new List<string>();
            foreach (var uploadFile in fileList)
            {
                var file = new RecordFile
                {
                    FileName = uploadFile["fileName"].ToString("D8"),
                    Configs = recordConfigs
                };
                fileNameList.Add(uploadFile["fileName"].ToString("D8"));
                using (var reader = new BinaryReader(new FileStream(files[uploadFile["file"]], FileMode.Open)))
                {
                    file.FileDataBytes = new byte[(int)(reader.BaseStream.Length - uploadFile["offset"] * 16)];
                    reader.BaseStream.Seek(uploadFile["offset"] * 16, SeekOrigin.Begin);
                    reader.Read(file.FileDataBytes, 0, (int)(reader.BaseStream.Length - uploadFile["offset"] * 16));
                }

                var response = string.Empty;
                var tryTimes = 0;
                while (response != "255")
                {
                    if (tryTimes > 10) return;
                    response = new PostClient($"{ServerAddr}RecordFile").Post(JsonConvert.SerializeObject(file));
                    tryTimes++;
                }

                _fileProcessed++;
                progress.UpdateProgressBar((_fileProcessed / (_fileCount * 1.0)) * 100);
            }
            StartRecordProcess(recordConfigs, progress, fileNameList);
        }

        private static void StartRecordProcess(Dictionary<string, string> recordConfigs, View.Progress progress, List<string> fileList )
        {
            var client = new PostClient($"{ServerAddr}Record");
            var recordParams = new RecordParams
            {
                RecordConfigs = recordConfigs,
                FileList = fileList,
                RecordName = recordConfigs["RecordName"]
            };

            var processId = Guid.Parse(client.Post(JsonConvert.SerializeObject(recordParams)).Replace("\"", string.Empty));
            var stage = string.Empty;
            client.SetParams($"{{record}}:{processId}");
            while (stage != RecordProcessStage.ProcessCompleted && stage != RecordProcessStage.Failed)
            {
                var fatchClient = new PostClient($"{ServerAddr}Record?record={processId}");
                stage = fatchClient.Get().Replace("\"", string.Empty);
                progress.UPdateProgressStage(stage);
                Thread.Sleep(1000);
            }

            progress.FinishUpload();
        }
    }
}
