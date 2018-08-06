using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XDetroit.WebFrontend.Interfaces;

namespace XDetroit.Testing.Behavioural.Providers
{
    public class InMemoryDataProvider<TContext> : IDataProvider
    {
        private Dictionary<Type, List<object>> entitiesCollections = new Dictionary<Type, List<object>>();
        private int Changes;
        private NavigationPropertiesGenerator<TContext> ONavigationPropertiesGenerator;

        public InMemoryDataProvider()
        {
            ONavigationPropertiesGenerator = new NavigationPropertiesGenerator<TContext>(entitiesCollections);
        }

        public IQueryable<T> GetEntities<T>() where T : class
        {
            var type = typeof(T);
            var returnValue = Enumerable.Empty<T>();
            if (entitiesCollections.ContainsKey(type))
            {
                var list = entitiesCollections[type];
                IEnumerable<T> entityCollection = list.Cast<T>();

                var populatedEntities = new List<T>();
                foreach (T entity in entityCollection)
                {
                    populatedEntities.Add(ONavigationPropertiesGenerator.PopulateNavigationProperties(entity));
                }

                returnValue = populatedEntities;
            }

            return returnValue.AsQueryable();
        }

        public T CreateEntity<T>(T entity) where T : class
        {
            Init<T>();

            if (entity is IEnumerable)
            {
                throw new ArgumentException("Argument cannot be a collection.");
            }

            entitiesCollections[typeof(T)].Add(entity);
            Changes++;

            return entity;
        }

        public IEnumerable<T> CreateEntities<T>(IEnumerable<T> entities) where T : class
        {
            Init<T>();
            entitiesCollections[typeof(T)].AddRange(entities);
            Changes += entities.Count();

            return entities;
        }

        private void Init<T>()
        {
            if (!entitiesCollections.ContainsKey(typeof(T)))
            {
                entitiesCollections.Add(typeof(T), new List<object>());
            }
        }

        public int SaveChanges()
        {
            foreach (var entitesCollectionItem in entitiesCollections)
            {
                var entitiesCollection = entitesCollectionItem.Value;

                string idPropertyTypeName = entitiesCollection.First().GetType().GetProperty("Id").PropertyType.Name;

                if (idPropertyTypeName.Equals("int32", StringComparison.InvariantCultureIgnoreCase))
                {
                    //TODO: code to generate keys. move to GenerateKeys() method
                    var entitiesCount = entitiesCollection.Count;
                    for (int i = 0; i < entitiesCount; i++)
                    {
                        var entityItem = entitiesCollection[i];
                        var entityItemType = entityItem.GetType();
                        PropertyInfo property = entityItemType.GetProperty("Id");

                        if (property == null)
                        {
                            throw new ApplicationException(
                                $"Entity format invalid. Entity {entityItem.GetType().Name} is missing the Id property.");
                        }

                        int value = (int)property.GetValue(entityItem);

                        if (value == 0)
                        {
                            property.SetValue(entityItem, entitiesCount + i + 1);
                        }
                    }
                }
                else
                {
                    //TODO: code to check unique key. move to CheckKeys() method
                }
            }

            var changesCount = Changes;
            Changes = 0;
            return changesCount;
        }

        public T UpdateEntity<T>(T entity) where T : class
        {
            int id = (int)entity.GetType().GetProperty("Id").GetValue(entity);
            var targetEntity = entitiesCollections[typeof(T)].First(x => GetId(x) == id);

            foreach (PropertyInfo property in targetEntity.GetType().GetProperties())
            {
                property.SetValue(targetEntity, entity.GetType().GetProperty(property.Name).GetValue(entity));
            }

            Changes++;

            return entity;
        }

        public T Find<T>(int id) where T : class
        {
            return GetEntities<T>().FirstOrDefault(x => GetId(x) == id);
        }
        
        private int GetId<T>(T entity)
        {
            return (int)entity.GetType().GetProperty("Id").GetValue(entity);
        }
    }
}