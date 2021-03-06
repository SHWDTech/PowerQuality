﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerQualityModel.DataModel
{
    /// <summary>
    /// 模块
    /// </summary>
    [Serializable]
    public class Module : SystemModel
    {
        [Display(Name = "所属父模块ID")]
        public virtual long? ParentModuleId { get; set; }

        [Display(Name = "所属父模块")]
        [ForeignKey("ParentModuleId")]
        public virtual Module ParentModule { get; set; }

        [Required]
        [Display(Name = "是否菜单项")]
        public bool IsMenu { get; set; }

        [Required]
        [Display(Name = "模块层级")]
        public virtual int ModuleLevel { get; set; }

        [Display(Name = "模块索引值")]
        public virtual int ModuleIndex { get; set; }

        [Display(Name = "菜单项图标字符串")]
        public virtual string IconString { get; set; }

        [Required]
        [Display(Name = "模块名称")]
        [MaxLength(25)]
        public virtual string ModuleName { get; set; }

        [Display(Name = "模块所属控制器")]
        [MaxLength(25)]
        public virtual string Controller { get; set; }

        [Display(Name = "模块所属操作")]
        [MaxLength(25)]
        public virtual string Action { get; set; }

        [Display(Name = "模块所属权限ID")]
        public virtual long? PermissionId { get; set; }

        [Display(Name = "模块所属权限")]
        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }
    }
}
