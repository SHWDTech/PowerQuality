using System.Collections.Generic;

namespace PowerQualityModel.ViewModel
{
    public class RecordFile
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件字节集合
        /// </summary>
        public List<byte> FileDataBytes { get; set; }

        /// <summary>
        /// 文件配置项
        /// </summary>
        public Dictionary<string, string> Configs { get; set; }
    }
}
