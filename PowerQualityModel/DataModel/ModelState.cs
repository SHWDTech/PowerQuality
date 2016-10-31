using System;
using System.ComponentModel.DataAnnotations;

namespace PowerQualityModel.DataModel
{
    /// <summary>
    /// 数据对象模型状态
    /// </summary>
    [Serializable]
    public enum ModelState : byte
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Display(Name = "新增")]
        Added = 0x00,

        /// <summary>
        /// 未改变
        /// </summary>
        [Display(Name = "未改变")]
        UnChanged = 0x01
    }
}
