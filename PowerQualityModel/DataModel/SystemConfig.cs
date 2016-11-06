using System.ComponentModel.DataAnnotations;

namespace PowerQualityModel.DataModel
{
    public class SystemConfig : SystemModel
    {
        [Required]
        [MaxLength(200)]
        public string ConfigType { get; set; }

        [Required]
        [MaxLength(200)]
        public string ConfigName { get; set; }

        [Required]
        [MaxLength(200)]
        public string ConfigValue { get; set; }
    }
}
