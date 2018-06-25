using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XDetroit.WebFrontend.Dal
{
    public class InMemoryDataProvider : IDataProvider
    {
        private Dictionary<Type, List<object>> entities = new Dictionary<Type, List<object>>();
        private int Changes;

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

        public T CreateEntity<T>(T entity) where T : class
        {
            Init<T>();

            entities[typeof(T)].Add(entity);
            Changes++;

            return entity;
        }

        private void Init<T>()
        {
            if (!entities.ContainsKey(typeof(T)))
            {
                entities.Add(typeof(T), new List<object>());
            }
        }

        public int SaveChanges()
        {
            foreach (var entity in entities)
            {
                var entityList = entity.Value;
                for (int i = 0; i < entityList.Count; i++)
                {
                    var entityItem = entityList[i];
                    var entityItemType = entityItem.GetType();
                    PropertyInfo property = entityItemType.GetProperty("Id");
                    property.SetValue(entityItem, i + 1);
                }
            }

            var changesCount = Changes;
            Changes = 0;
            return changesCount;
        }
    }
}