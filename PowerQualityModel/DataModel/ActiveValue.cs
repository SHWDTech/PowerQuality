using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable InconsistentNaming

namespace PowerQualityModel.DataModel
{
    /// <summary>
    /// 有效值记录
    /// </summary>
    public class ActiveValue : SystemModel
    {
        /// <summary>
        /// 所属记录GUID
        /// </summary>
        [Required]
        [Index("IX_RecordActiveValue", 1, IsClustered = true)]
        public Guid RecordGuid { get; set; }

        /// <summary>
        /// 所属记录
        /// </summary>
        [ForeignKey("RecordGuid")]
        public Record Record { get; set; }

        /// <summary>
        /// 记录序列号
        /// </summary>
        [Required]
        [Index("IX_RecordActiveValue", 2, IsClustered = true)]
        public int RecordIndex { get; set; }

        [Required]
        public long RecordTimeTicks { get; set; }

        [NotMapped]
        public DateTime RecordTime 
            => RecordTimeTicks != 0 ? new DateTime(RecordTimeTicks) : DateTime.MinValue;

        /// <summary>
        /// AN电压
        /// </summary>
        [Required]
        public double Voltage_AN { get; set; }

        /// <summary>
        /// BN电压
        /// </summary>
        [Required]
        public double Voltage_BN { get; set; }

        /// <summary>
        /// CN电压
        /// </summary>
        [Required]
        public double Voltage_CN { get; set; }

        /// <summary>
        /// 零地线电压
        /// </summary>
        [Required]
        public double Voltage_NG { get; set; }

        /// <summary>
        /// AB相电压
        /// </summary>
        [Required]
        public double Voltage_AB { get; set; }

        /// <summary>
        /// BC相电压
        /// </summary>
        [Required]
        public double Voltage_BC { get; set; }

        /// <summary>
        /// CA相电压
        /// </summary>
        [Required]
        public double Voltage_CA { get; set; }

        /// <summary>
        /// A线电流
        /// </summary>
        [Required]
        public double Current_A { get; set; }

        /// <summary>
        /// B线电流
        /// </summary>
        [Required]
        public double Current_B { get; set; }

        /// <summary>
        /// C线电流
        /// </summary>
        [Required]
        public double Current_C { get; set; }

        /// <summary>
        /// 零线电流
        /// </summary>
        [Required]
        public double Current_N { get; set; }

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
