﻿using System;
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
        /// 通道A有功功率
        /// </summary>
        [Required]
        public double ActivePowerA { get; set; }

        /// <summary>
        /// 通道A视在功率
        /// </summary>
        [Required]
        public double ApparentPowerA { get; set; }

        /// <summary>
        /// 通道A无功功率
        /// </summary>
        [Required]
        public double ReactivePowerA { get; set; }

        /// <summary>
        /// 通道A功率因素
        /// </summary>
        [Required]
        public double PowerFactorA { get; set; }

        /// <summary>
        /// 通道B有功功率
        /// </summary>
        [Required]
        public double ActivePowerB { get; set; }

        /// <summary>
        /// 通道B视在功率
        /// </summary>
        [Required]
        public double ApparentPowerB { get; set; }

        /// <summary>
        /// 通道B无功功率
        /// </summary>
        [Required]
        public double ReactivePowerB { get; set; }

        /// <summary>
        /// 通道B功率因素
        /// </summary>
        [Required]
        public double PowerFactorB { get; set; }

        /// <summary>
        /// 通道C有功功率
        /// </summary>
        [Required]
        public double ActivePowerC { get; set; }

        /// <summary>
        /// 通道C视在功率
        /// </summary>
        [Required]
        public double ApparentPowerC { get; set; }

        /// <summary>
        /// 通道C无功功率
        /// </summary>
        [Required]
        public double ReactivePowerC { get; set; }

        /// <summary>
        /// 通道C功率因素
        /// </summary>
        [Required]
        public double PowerFactorC { get; set; }

        /// <summary>
        /// 基波通道A有功功率
        /// </summary>
        [Required]
        public double BaseActivePowerA { get; set; }

        /// <summary>
        /// 基波通道A视在功率
        /// </summary>
        [Required]
        public double BaseApparentPowerA { get; set; }

        /// <summary>
        /// 基波通道A无功功率
        /// </summary>
        [Required]
        public double BaseReactivePowerA { get; set; }

        /// <summary>
        /// 基波通道A功率因素
        /// </summary>
        [Required]
        public double BasePowerFactorA { get; set; }

        /// <summary>
        /// 基波通道B有功功率
        /// </summary>
        [Required]
        public double BaseActivePowerB { get; set; }

        /// <summary>
        /// 基波通道B视在功率
        /// </summary>
        [Required]
        public double BaseApparentPowerB { get; set; }

        /// <summary>
        /// 基波通道B无功功率
        /// </summary>
        [Required]
        public double BaseReactivePowerB { get; set; }

        /// <summary>
        /// 基波通道B功率因素
        /// </summary>
        [Required]
        public double BasePowerFactorB { get; set; }

        /// <summary>
        /// 基波通道C有功功率
        /// </summary>
        [Required]
        public double BaseActivePowerC { get; set; }

        /// <summary>
        /// 基波通道C视在功率
        /// </summary>
        [Required]
        public double BaseApparentPowerC { get; set; }

        /// <summary>
        /// 基波通道C无功功率
        /// </summary>
        [Required]
        public double BaseReactivePowerC { get; set; }

        /// <summary>
        /// 基波通道C功率因素
        /// </summary>
        [Required]
        public double BasePowerFactorC { get; set; }

        /// <summary>
        /// A通道相位角
        /// </summary>
        [Required]
        public double VoltagePhaseAngleA { get; set; }

        /// <summary>
        /// B通道相位角
        /// </summary>
        [Required]
        public double VoltagePhaseAngleB { get; set; }

        /// <summary>
        /// C通道相位角
        /// </summary>
        [Required]
        public double VoltagePhaseAngleC { get; set; }

        /// <summary>
        /// A通道相位角
        /// </summary>
        [Required]
        public double CurrentPhaseAngleA { get; set; }

        /// <summary>
        /// B通道相位角
        /// </summary>
        [Required]
        public double CurrentPhaseAngleB { get; set; }

        /// <summary>
        /// C通道相位角
        /// </summary>
        [Required]
        public double CurrentPhaseAngleC { get; set; }

        /// <summary>
        /// A通道相位角
        /// </summary>
        [Required]
        public double BaseVoltagePhaseAngleA { get; set; }

        /// <summary>
        /// B通道相位角
        /// </summary>
        [Required]
        public double BaseVoltagePhaseAngleB { get; set; }

        /// <summary>
        /// C通道相位角
        /// </summary>
        [Required]
        public double BaseVoltagePhaseAngleC { get; set; }

        /// <summary>
        /// A通道相位角
        /// </summary>
        [Required]
        public double BaseCurrentPhaseAngleA { get; set; }

        /// <summary>
        /// B通道相位角
        /// </summary>
        [Required]
        public double BaseCurrentPhaseAngleB { get; set; }

        /// <summary>
        /// C通道相位角
        /// </summary>
        [Required]
        public double BaseCurrentPhaseAngleC { get; set; }

        /// <summary>
        /// 电压负向不平衡率
        /// </summary>
        [Required]
        public double VoltageU2 { get; set; }

        /// <summary>
        /// 电压零向不平衡率
        /// </summary>
        [Required]
        public double VoltageU0 { get; set; }

        /// <summary>
        /// 电流负向不平衡率
        /// </summary>
        [Required]
        public double CurrentU2 { get; set; }

        /// <summary>
        /// 电流零向不平衡率
        /// </summary>
        [Required]
        public double CurrentU0 { get; set; }

        /// <summary>
        /// 是否存在闪变
        /// </summary>
        [Required]
        public bool HasFlicker { get; set; }

        /// <summary>
        /// 是否存在浪涌
        /// </summary>
        [Required]
        public bool HasSurge { get; set; }
    }
}
