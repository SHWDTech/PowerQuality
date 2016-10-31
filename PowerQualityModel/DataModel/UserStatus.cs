using System;
using System.ComponentModel.DataAnnotations;

namespace PowerQualityModel.DataModel
{
    /// <summary>
    /// 用户状态
    /// </summary>
    [Serializable]
    public enum UserStatus : byte
    {
        /// <summary>
        /// 已经启用
        /// </summary>
        [Display(Name = "已启用")]
        Enabled = 0x00,

        /// <summary>
        /// 已经禁用
        /// </summary>
        [Display(Name = "已禁用")]
        Disabled = 0x01,

        /// <summary>
        /// 已经停用
        /// </summary>
        [Display(Name = "已停用")]
        Stopped = 0x02,

        /// <summary>
        /// 已经锁定
        /// </summary>
        [Display(Name = "已锁定")]
        Locked = 0x03
    }
}
