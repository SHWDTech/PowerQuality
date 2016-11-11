using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace PowerQualityUploader
{
    public class FileUpLoader
    {
        public static Dictionary<string, string> ConfigRequirements { get; private set; }

        public static string ServerAddr { get; set; } = string.Empty;

        private static bool TryParseConfig(string configString, out Dictionary<string, string> config)
        {
            try
            {
                var items = configString.Split(new[] { "\r\n" }, StringSplitOptions.None);
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

        public static void LoadRecord(string directory)
        {
            var directories = Directory.GetDirectories(directory);
            foreach (var dir in directories.Where(obj => File.Exists($"{obj}\\power.cfg")))
            {
                Dictionary<string, string> recordConfigs;
                if (TryParseConfig(File.ReadAllText($"{dir}\\power.cfg"), out recordConfigs))
                {

                }
            }
        }

        public static void UpdateConfigRequirements()
        {
            var postClient = new PostClient($"{ServerAddr}RecordConfig");
            var responseJosn = postClient.Get();
            ConfigRequirements = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseJosn);
           File.WriteAllText($"{Directory.GetCurrentDirectory()}\\configs.json", JsonConvert.SerializeObject(ConfigRequirements, Formatting.Indented));
        }
    }
}
