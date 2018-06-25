using System;
using System.Collections.Generic;
using System.Linq;

namespace XDetroit.WebFrontend.Dal
{
    public class DataProvider : IDataProvider
    {
        private Dictionary<Type, List<object>> entities = new Dictionary<Type, List<object>>();
        
        public IQueryable<T> GetEntities<T>() where T : class
        {
            var type = typeof(T);
            var returnValue = Enumerable.Empty<T>();
            if (entities.ContainsKey(type))
            {
                var list = entities[type];
                IEnumerable<T> entityCollection = list.Cast<T>();
                returnValue = entityCollection;
            }

            return returnValue.AsQueryable();
        }
    }
}