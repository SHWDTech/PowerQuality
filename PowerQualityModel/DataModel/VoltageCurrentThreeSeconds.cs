﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable InconsistentNaming

namespace PowerQualityModel.DataModel
{
    public class VoltageCurrentThreeSeconds : SystemModel
    {
        [Key]
        [Display(Name = "唯一标识符")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override long Id { get; set; }

        [Required]
        public long RecordId { get; set; }

        [ForeignKey("RecordId")]
        public Record Record { get; set; }

        [Required]
        public int RecordIndex { get; set; }

        [NotMapped]
        public double RecordTime
            => Record.RecordStartDateTime.AddMilliseconds(RecordIndex * 3000 )
            .ToUniversalTime()
            .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

        public double Voltage_AN_Max { get; set; }

        public double Voltage_AN_Avg { get; set; }

        public double Voltage_AN_Min { get; set; }

        public double Voltage_BN_Max { get; set; }

        public double Voltage_BN_Avg { get; set; }

        public double Voltage_BN_Min { get; set; }

        public double Voltage_CN_Max { get; set; }

        public double Voltage_CN_Avg { get; set; }

        public double Voltage_CN_Min { get; set; }

        public double Voltage_NG_Max { get; set; }

        public double Voltage_NG_Avg { get; set; }

        public double Voltage_NG_Min { get; set; }

        public double Voltage_AB_Max { get; set; }

        public double Voltage_AB_Avg { get; set; }

        public double Voltage_AB_Min { get; set; }

        public double Voltage_BC_Max { get; set; }

        public double Voltage_BC_Avg { get; set; }

        public double Voltage_BC_Min { get; set; }

        public double Voltage_CA_Max { get; set; }

        public double Voltage_CA_Avg { get; set; }

        public double Voltage_CA_Min { get; set; }

        public double Current_A_Max { get; set; }

        public double Current_A_Avg { get; set; }

        public double Current_A_Min { get; set; }

        public double Current_B_Max { get; set; }

        public double Current_B_Avg { get; set; }

        public double Current_B_Min { get; set; }

        public double Current_C_Max { get; set; }

        public double Current_C_Avg { get; set; }

        public double Current_C_Min { get; set; }

        public double Current_N_Max { get; set; }

        public double Current_N_Avg { get; set; }

        public double Current_N_Min { get; set; }

        public double ActivePower_A_Max { get; set; }

        public double ActivePower_A_Avg { get; set; }

        public double ActivePower_A_Min { get; set; }

        public double ActivePower_B_Max { get; set; }

        public double ActivePower_B_Avg { get; set; }

        public double ActivePower_B_Min { get; set; }

        public double ActivePower_C_Max { get; set; }

        public double ActivePower_C_Avg { get; set; }

        public double ActivePower_C_Min { get; set; }

        public double ReactivePower_A_Max { get; set; }

        public double ReactivePower_A_Avg { get; set; }

        public double ReactivePower_A_Min { get; set; }

        public double ReactivePower_B_Max { get; set; }

        public double ReactivePower_B_Avg { get; set; }

        public double ReactivePower_B_Min { get; set; }

        public double ReactivePower_C_Max { get; set; }

        public double ReactivePower_C_Avg { get; set; }

        public double ReactivePower_C_Min { get; set; }

        public double ApparentPower_A_Max { get; set; }

        public double ApparentPower_A_Avg { get; set; }

        public double ApparentPower_A_Min { get; set; }

        public double ApparentPower_B_Max { get; set; }

        public double ApparentPower_B_Avg { get; set; }

        public double ApparentPower_B_Min { get; set; }

        public double ApparentPower_C_Max { get; set; }

        public double ApparentPower_C_Avg { get; set; }

        public double ApparentPower_C_Min { get; set; }

        public double PowerFactor_A_Max { get; set; }

        public double PowerFactor_A_Avg { get; set; }

        public double PowerFactor_A_Min { get; set; }

        public double PowerFactor_B_Max { get; set; }

        public double PowerFactor_B_Avg { get; set; }

        public double PowerFactor_B_Min { get; set; }

        public double PowerFactor_C_Max { get; set; }

        public double PowerFactor_C_Avg { get; set; }

        public double PowerFactor_C_Min { get; set; }
    }
}
