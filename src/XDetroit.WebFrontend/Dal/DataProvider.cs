using System.Linq;

namespace XDetroit.WebFrontend.Dal
{
    public class DataProvider : IDataProvider
    {
        public IQueryable<T> GetEntities<T>() where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}