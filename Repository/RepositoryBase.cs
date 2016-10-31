using System.Threading;
using PowerQualityModel;
using PowerQualityModel.DataModel;

namespace Repository
{
    public class RepositoryBase
    {
        /// <summary>
        /// 初始化数据仓库基类
        /// </summary>
        protected RepositoryBase()
        {
        }

        /// <summary>
        /// 数据仓库上下文
        /// </summary>
        public static IRepositoryContext RepositoryContext => ContextLocal == null ? ContextGlobal : ContextLocal.Value;

        /// <summary>
        /// 当前线程的用户
        /// </summary>
        public static PowerUser CurrentUser => RepositoryContext.CurrentUser;

        /// <summary>
        /// 数据仓库上下文线程对象
        /// </summary>
        public static ThreadLocal<IRepositoryContext> ContextLocal { get; set; }

        /// <summary>
        /// 全局数据仓库上下文线程对象
        /// </summary>
        public static IRepositoryContext ContextGlobal { get; set; }

        public static PowerDbContext BaseContext = new PowerDbContext();
    }
}
