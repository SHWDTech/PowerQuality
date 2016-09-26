using System;
using System.ComponentModel.DataAnnotations;

namespace PowerQualityModel
{
    /// <summary>
    /// 电力采集记录
    /// </summary>
    [Serializable]
    public class Record : SystemModel
    {
        /// <summary>
        /// 记录名称
        /// </summary>
        [Required]
        [MaxLength(200)]
        [Display(Name = "记录名称")]
        public virtual string RecordName { get; set; }

        /// <summary>
        /// 记录上传时间
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "记录上传时间")]
        public virtual DateTime RecordDateTime { get; set; }

        /// <summary>
        /// 记录开始时间
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "记录上传时间")]
        public virtual DateTime RecordStartDateTime { get; set; }
    }
}
