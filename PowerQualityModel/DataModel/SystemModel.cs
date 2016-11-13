using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace PowerQualityModel.DataModel
{
    /// <summary>
    /// 系统模型类
    /// </summary>
    [Serializable]
    public class SystemModel : ModelBase
    {
        [Key]
        [Display(Name = "唯一标识符")]
        public override long Id { get; set; }

        [NotMapped]
        [Display(Name = "模型状态")]
        public override ModelState ModelState { get; set; } = ModelState.Added;

        [NotMapped]
        [Display(Name = "是否新创建对象")]
        [ScriptIgnore]
        public override bool IsNew => ModelState == ModelState.Added;

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
