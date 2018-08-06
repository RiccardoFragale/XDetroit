using System.Linq;
using $rootnamespace$.Interfaces;

namespace $rootnamespace$.Providers
{
    public class SampleDataProvider //: IDataProvider
    {
        //private AppContext DbContext { get; }

        //public SampleDataProvider(AppContext dbContext)
        //{
        //    DbContext = dbContext;
        //}

        //public T CreateEntity<T>(T entity) where T : class
        //{
        //    return DbContext.Set<T>().Add(entity);
        //}

        //public IQueryable<T> GetEntities<T>() where T : class
        //{
        //    return DbContext.Set<T>();
        //}

        //public T Find<T>(int id) where T : class
        //{
        //    return DbContext.Set<T>().Find(id);
        //}

        //public T UpdateEntity<T>(T entity) where T : class
        //{
        //    DbContext.Entry(entity).State = EntityState.Modified;

        //    return entity;
        //}

		//public IEnumerable<T> CreateEntities<T>(IEnumerable<T> entities) where T : class
        //{
        //    return DbContext.Set<T>().AddRange(entities);
        //}


        //public int SaveChanges()
        //{
        //    return DbContext.SaveChanges();
        //}
    }

	public class AppContext //: DbContext
    {
		//Your Entity framework model class, or other class using the actual ORM's interfaces/classes.

        public AppContext()
           // : base("name=AppContext")
        {
        }

        //public virtual DbSet<ItemCategory> Categories { get; set; }
        //public virtual DbSet<ProductItem> Products { get; set; }
    }
}