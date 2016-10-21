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
            Database.SetInitializer(new DropCreateDatabaseAlways<PowerDbContext>());
        }

        public PowerDbContext(bool dropCreate) : base("Power_Quality")
        {
            
        }

        /// <summary>
        /// 创建默认的DbContext
        /// </summary>
        /// <param name="connString">连接字符串或连接字符串名称</param>
        public PowerDbContext(string connString) : base(connString)
        {
            
        }

        public virtual DbSet<PowerRole> Roles { get; set; }

        public virtual DbSet<PowerUser> Users { get; set; }

        public virtual DbSet<Module> Modules { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<Record> Records { get; set; }

        public virtual DbSet<RecordConfig> RecordConfigs { get; set; }

        public virtual DbSet<ActiveValue> ActiveValues { get; set; }
    }
}
