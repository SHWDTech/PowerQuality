using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerQualityModel.DataModel
{
    [Serializable]
    public abstract class ModelBase
    {
        /// <summary>
        /// 唯一标识符
        /// </summary>
        public abstract Guid Id { get; set; }

        /// <summary>
        /// 模型状态
        /// </summary>
        public abstract ModelState ModelState { get; set; }

        /// <summary>
        /// 是否新创建对象
        /// </summary>
        [NotMapped]
        public abstract bool IsNew { get; set; }

        /// <summary>
        /// 转化为JSON字符串
        /// </summary>
        /// <returns></returns>
        public abstract string ToJson();
    }
}
