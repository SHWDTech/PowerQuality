using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PowerQualityModel
{
    /// <summary>
    /// 系统模型类
    /// </summary>
    [Serializable]
    public class SystemModel : ModelBase
    {
        [Key]
        [Display(Name = "唯一标识符")]
        public override Guid Id { get; set; }

        [NotMapped]
        [Display(Name = "模型状态")]
        public override ModelState ModelState { get; set; }

        [NotMapped]
        [Display(Name = "是否新创建对象")]
        public override bool IsNew { get; set; }

        /// <summary>
        /// 模型创建时间
        /// </summary>
        [JsonIgnore]
        [DataType(DataType.DateTime)]
        [Display(Name = "模型创建时间")]
        public virtual DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 模型创建用户ID
        /// </summary>
        [JsonIgnore]
        [Display(Name = "模型创建用户ID")]
        public virtual Guid CreateUserId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [JsonIgnore]
        [DataType(DataType.DateTime)]
        [Display(Name = "最后修改时间")]
        public virtual DateTime? LastUpdateDateTime { get; set; }

        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        [JsonIgnore]
        [Display(Name = "最后修改用户ID")]
        public virtual Guid? LastUpdateUserId { get; set; }

        /// <summary>
        /// 是否标记为删除
        /// </summary>
        [JsonIgnore]
        [Display(Name = "是否标记为删除")]
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 是否标记为启用
        /// </summary>
        [Display(Name = "是否启用")]
        public virtual bool IsEnabled { get; set; }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
