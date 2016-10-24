using System.Data.Entity;
using PowerQualityModel;

namespace Repository
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class PowerDbContext : DbContext
    {
        /// <summary>
        /// 创建默认的DbContext
        /// </summary>
        public PowerDbContext() : base("Power_Quality")
        {
            
        }

        /// <summary>
        /// 创建默认的DbContext
        /// </summary>
        /// <param name="connString">连接字符串或连接字符串名称</param>
        public PowerDbContext(string connString) : base(connString)
        {
            
        }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual DbSet<PowerRole> Roles { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual DbSet<PowerUser> Users { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public virtual DbSet<Module> Modules { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public virtual DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// 记录
        /// </summary>
        public virtual DbSet<Record> Records { get; set; }

        /// <summary>
        /// 记录配置项
        /// </summary>
        public virtual DbSet<RecordConfig> RecordConfigs { get; set; }

        /// <summary>
        /// 电力系统记录值
        /// </summary>
        public virtual DbSet<ActiveValue> ActiveValues { get; set; }
    }
}
