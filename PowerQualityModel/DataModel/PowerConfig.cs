using System;
using System.Collections.Generic;

namespace PowerQualityModel.DataModel
{
    public class PowerConfig
    {
        private readonly Dictionary<string, string> _configs = new Dictionary<string, string>();

        public PowerConfig(string configString)
        {
            var configs = configString.Split(new[] { "\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var config in configs)
            {
                var keyvalue = config.Split('=');
                _configs.Add(keyvalue[0], keyvalue[1]);
            }
        }

        public string this[string name]
        {
            get
            {
                return _configs.ContainsKey(name) ? _configs[name] : string.Empty;
            }
            set
            {
                if (_configs.ContainsKey(name))
                {
                    _configs[name] = value;
                }
            }
        }
    }
}
