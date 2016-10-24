using Repository;

namespace PowerProcess
{
    public class ProcessBase
    {
        public PowerDbContext DbContext { get; }

        public ProcessBase()
        {
            DbContext = new PowerDbContext();
        }

        protected ProcessBase(string connString) : this()
        {
            DbContext = new PowerDbContext(connString);
        }

        protected ProcessBase(PowerDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public T Repo<T>() where T : RepositoryBase,IRepository, new()
        {
            var repo = new T { DbContext = DbContext};
            return repo;
        }
    }
}
