using System.ComponentModel.DataAnnotations;

// ReSharper disable InconsistentNaming

namespace PowerQualityModel.ViewModel
{
    public class ToolBoxModel
    {
        [Display(Name = "AN(V)")]
        public bool Voltage_AN { get; set; }

        [Display(Name = "BN")]
        public bool Voltage_BN { get; set; }

        [Display(Name = "CN(V)")]
        public bool Voltage_CN { get; set; }

        [Display(Name = "NG")]
        public bool Voltage_NG { get; set; }

        [Display(Name = "AB(V)")]
        public bool Voltage_AB { get; set; }

        [Display(Name = "BC(V)")]
        public bool Voltage_BC { get; set; }

        [Display(Name = "VA(V)")]
        public bool Voltage_CA { get; set; }

        [Display(Name = "A(A)")]
        public bool Current_A { get; set; }

        [Display(Name = "B(A)")]
        public bool Current_B { get; set; }

        [Display(Name = "C(A)")]
        public bool Current_C { get; set; }

        [Display(Name = "N(A)")]
        public bool Current_N { get; set; }
    }
}