namespace Repository
{
    public interface IRepository
    {
        /// <summary>
        /// 数据仓库上下文
        /// </summary>
        PowerDbContext DbContext { get; set; }

        /// <summary>
        /// 初始化数据实体对象
        /// </summary>
        void InitEntitySet();
    }
}
