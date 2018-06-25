using System.Linq;

namespace XDetroit.WebFrontend.Dal
{
    public interface IDataProvider
    {
        T CreateEntity<T>(T entity) where T : class;
        IQueryable<T> GetEntities<T>() where T : class;
        int SaveChanges();
    }
}