using PowerQualityModel;

namespace Repository
{
    /// <summary>
    /// 数据仓库上下文信息接口
    /// </summary>
    public interface IRepositoryContext
    {
        /// <summary>
        /// 当前操作用户
        /// </summary>
        PowerUser CurrentUser { get; set; }
    }
}
