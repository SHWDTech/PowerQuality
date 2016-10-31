using System;
using System.ComponentModel.DataAnnotations;

namespace PowerQualityModel.DataModel
{
    /// <summary>
    /// 角色状态
    /// </summary>
    [Serializable]
    public enum RoleStatus : byte
    {
        /// <summary>
        /// 已启用
        /// </summary>
        [Display(Name = "已启用")]
        Enabled = 0x01,

        /// <summary>
        /// 已禁用
        /// </summary>
        [Display(Name = "已禁用")]
        Disabled = 0x02,

        /// <summary>
        /// 已暂停
        /// </summary>
        [Display(Name = "已暂停")]
        Stopped = 0x03,

        /// <summary>
        /// 已锁定
        /// </summary>
        [Display(Name = "已锁定")]
        Locked = 0x04
    }
}
