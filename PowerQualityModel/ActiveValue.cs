using System;
using System.ComponentModel.DataAnnotations;

namespace PowerQualityModel
{
    /// <summary>
    /// 有效值记录
    /// </summary>
    public class ActiveValue
    {
        /// <summary>
        /// 所属记录GUID
        /// </summary>
        [Required]
        public Guid RecordGuid { get; set; }

        /// <summary>
        /// 所属记录
        /// </summary>
        public Record Record { get; set; }

        /// <summary>
        /// 记录序列号
        /// </summary>
        [Required]
        public int Index { get; set; }

        /// <summary>
        /// 一通道数据
        /// </summary>
        [Required]
        public double ChannelOne { get; set; }

        /// <summary>
        /// 二通道数据
        /// </summary>
        [Required]
        public double ChannelTwo { get; set; }

        /// <summary>
        /// 三通道数据
        /// </summary>
        [Required]
        public double ChannelThree { get; set; }

        /// <summary>
        /// 四通道数据
        /// </summary>
        [Required]
        public double ChannelFour { get; set; }

        /// <summary>
        /// 五通道数据
        /// </summary>
        [Required]
        public double ChannelFive { get; set; }

        /// <summary>
        /// 六通道数据
        /// </summary>
        [Required]
        public double ChannelSix { get; set; }

        /// <summary>
        /// 七通道数据
        /// </summary>
        [Required]
        public double ChannelSeven { get; set; }

        /// <summary>
        /// 八通道数据
        /// </summary>
        [Required]
        public double ChannelEight { get; set; }

        /// <summary>
        /// 频率
        /// </summary>
        [Required]
        public double Frequency { get; set; }

        /// <summary>
        /// 有功功率
        /// </summary>
        [Required]
        public double ActivePowerOne { get; set; }

        /// <summary>
        /// 视在功率
        /// </summary>
        [Required]
        public double ApparentPowerOne { get; set; }

        /// <summary>
        /// 无功功率
        /// </summary>
        [Required]
        public double ReactivePowerOne { get; set; }

        /// <summary>
        /// 功率因素
        /// </summary>
        [Required]
        public double PowerFactorOne { get; set; }

        /// <summary>
        /// 有功功率
        /// </summary>
        [Required]
        public double ActivePowerTwo { get; set; }

        /// <summary>
        /// 视在功率
        /// </summary>
        [Required]
        public double ApparentPowerTwo { get; set; }

        /// <summary>
        /// 无功功率
        /// </summary>
        [Required]
        public double ReactivePowerTwo { get; set; }

        /// <summary>
        /// 功率因素
        /// </summary>
        [Required]
        public double PowerFactorTwo { get; set; }

        /// <summary>
        /// 有功功率
        /// </summary>
        [Required]
        public double ActivePowerThree { get; set; }

        /// <summary>
        /// 视在功率
        /// </summary>
        [Required]
        public double ApparentPowerThree { get; set; }

        /// <summary>
        /// 无功功率
        /// </summary>
        [Required]
        public double ReactivePowerThree { get; set; }

        /// <summary>
        /// 功率因素
        /// </summary>
        [Required]
        public double PowerFactorThree { get; set; }
    }
}
