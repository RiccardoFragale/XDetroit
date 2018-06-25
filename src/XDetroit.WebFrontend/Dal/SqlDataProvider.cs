using System.Data.Entity;
using System.Linq;

namespace XDetroit.WebFrontend.Dal
{
    public class SqlDataProvider : IDataProvider
    {
        private DbContext DbContext { get; }

        public SqlDataProvider(AppContext dbContext)
        {
            DbContext = dbContext;
        }

        public T CreateEntity<T>(T entity) where T : class
        {
            return DbContext.Set<T>().Add(entity);
        }

        public IQueryable<T> GetEntities<T>() where T : class
        {
            return DbContext.Set<T>();
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }
    }
}