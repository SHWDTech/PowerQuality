using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerQualityModel.DataModel
{
    /// <summary>
    /// 采集记录配置信息
    /// </summary>
    public class RecordConfig : SystemModel
    {
        /// <summary>
        /// 接线方式
        /// </summary>
        [Required]
        public LineType LineType { get; set; }

        /// <summary>
        /// 采集频率
        /// </summary>
        [Required]
        public ushort Frequency { get; set; }

        /// <summary>
        /// 计算精度
        /// </summary>
        [Required]
        public ushort CalcPrecision { get; set; }

        /// <summary>
        /// 记录对应ID
        /// </summary>
        [Required]
        public long RecordGuid { get; set; }

        /// <summary>
        /// 配置项对应采集记录
        /// </summary>
        [ForeignKey("RecordGuid")]
        public Record Record { get; set; }
    }
}
