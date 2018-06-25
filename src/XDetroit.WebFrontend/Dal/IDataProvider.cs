using System.Linq;

namespace XDetroit.WebFrontend.Dal
{
    public interface IDataProvider
    {
        IQueryable<T> GetEntities<T>() where T : class;
    }
}