using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using XDetroit.WebFrontend.Interfaces;

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

        public T Find<T>(int id) where T : class
        {
            return DbContext.Set<T>().Find(id);
        }

        public T UpdateEntity<T>(T entity) where T : class
        {
            DbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public IEnumerable<T> CreateEntities<T>(IEnumerable<T> entities) where T : class
        {
            return DbContext.Set<T>().AddRange(entities);
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }
    }
}