using System.Data.Entity;
using PowerQualityModel.DataModel;

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
            //Database.SetInitializer(new DropCreateDatabaseAlways<PowerDbContext>());
            //ProviderFactory.Register<MySqlProvider>("MySql.Data.MySqlClient.MySqlConnection");
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

        /// <summary>
        /// 谐波信息记录值
        /// </summary>
        public virtual DbSet<Harmonic> Harmonics { get; set; }

        /// <summary>
        /// 电流电压每秒统计值
        /// </summary>
        public virtual DbSet<VoltageCurrentSecond> VoltageCurrentSeconds { get; set; }

        /// <summary>
        /// 电流电压三秒统计值
        /// </summary>
        public virtual DbSet<VoltageCurrentThreeSeconds> VoltageCurrentThreeSecondses { get; set; }

    }
}
