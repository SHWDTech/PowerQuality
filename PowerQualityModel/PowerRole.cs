using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerQualityModel
{
    /// <summary>
    /// 角色
    /// </summary>
    [Serializable]
    public class PowerRole : SystemModel
    {
        [Display(Name = "父级角色ID")]
        public virtual Guid? ParentRoleId { get; set; }

        [Display(Name = "父级角色")]
        [ForeignKey("ParentRoleId")]
        public virtual PowerRole ParentRole { get; set; }

        [Required]
        [Display(Name = "角色名")]
        [MaxLength(25)]
        public virtual string RoleName { get; set; }

        [Display(Name = "是否可见")]
        public virtual bool IsVisiable { get; set; } = true;

        [Display(Name = "包含用户")]
        public virtual IEnumerable<PowerUser> Users { get; set; } = new List<PowerUser>();

        [Required]
        [Display(Name = "角色状态")]
        public virtual RoleStatus Status { get; set; }

        [Display(Name = "角色描述")]
        public virtual string Comments { get; set; }

        [Display(Name = "包含权限")]
        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
