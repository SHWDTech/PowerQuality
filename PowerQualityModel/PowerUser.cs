using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;

namespace PowerQualityModel
{
    /// <summary>
    /// 用户
    /// </summary>
    [Serializable]
    public class PowerUser : SystemModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [MaxLength(25)]
        [Display(Name = "用户名")]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 用户登录名
        /// </summary>
        [Required]
        [Index(IsUnique = true)]
        [MaxLength(25)]
        [Display(Name = "登录名")]
        public virtual string LoginName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(25)]
        [Display(Name = "姓名")]
        public virtual string UserIdentityName { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Display(Name = "登陆密码")]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "邮箱地址")]
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(15)]
        [Display(Name = "手机号码")]
        [DataType(DataType.PhoneNumber)]
        public virtual string Telephone { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [Display(Name = "最后登陆时间")]
        [DataType(DataType.DateTime)]
        public virtual DateTime? LastLoginDateTime { get; set; }

        /// <summary>
        /// 用户是否可见
        /// </summary>
        [Display(Name = "是否可见")]
        public virtual bool IsVisiable { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        [Required]
        [Display(Name = "状态")]
        public virtual UserStatus Status { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        [Display(Name = "所属角色")]
        public virtual IEnumerable<PowerRole> Roles { get; set;}  = new List<PowerRole>();

        /// <summary>
        /// 用户拥有的权限
        /// </summary>
        [Display(Name = "拥有的权限")]
        public virtual IEnumerable<Permission> Permissions { get; set; }

        /// <summary>
        /// 用户是否属于某个角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual bool IsInRole(string roleName) => Roles.Any(role => role.RoleName == roleName);

        [NotMapped]
        public virtual IIdentity Identity { get; set; }
    }
}
