using System.Linq;

namespace XDetroit.WebFrontend.Interfaces
{
    public interface IDataProvider
    {
        T CreateEntity<T>(T entity) where T : class;
        IQueryable<T> GetEntities<T>() where T : class;
        T Find<T>(int id) where T : class;
        T UpdateEntity<T>(T entity) where T : class;
        int SaveChanges();
    }
}