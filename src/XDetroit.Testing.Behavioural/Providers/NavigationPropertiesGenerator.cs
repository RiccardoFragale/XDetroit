using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XDetroit.Testing.Behavioural.Providers
{
    public class NavigationPropertiesGenerator<TContext>
    {
        readonly Dictionary<Type, List<object>> Entities;
        private readonly List<string> EntitiesTypesNames;

        public NavigationPropertiesGenerator(Dictionary<Type, List<object>> entities)
        {
            EntitiesTypesNames = typeof(TContext).GetProperties().AsEnumerable()
                .Where(x => x.PropertyType.Name.Equals("DbSet`1", StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.PropertyType.GetGenericArguments()[0].Name).ToList();

            Entities = entities;
        }

        public T PopulateNavigationProperties<T>(T entity) where T : class
        {
            var entityType = typeof(T);

            var properties = entityType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string propertyTypeName = property.PropertyType.Name;
                bool isCollection = IsCollectionTypeName(propertyTypeName);

                Type navigationPropertyType = GetNavigationPropertyType(isCollection, property);

                if (EntitiesTypesNames.Contains(navigationPropertyType.Name))
                {
                    object value;
                    if (isCollection)
                    {
                        value = GetCollectionNavigationPropertyValue<T, object>(entity, navigationPropertyType);
                    }
                    else
                    {
                        value = GetNavigationPropertyValue(entity, property);
                    }

                    PropertyInfo entityPropertyInfo = entity.GetType().GetProperty(property.Name);
                    entityPropertyInfo.SetValue(entity, value);
                }
                else
                {
                    if (isCollection)
                    {
                        throw new ApplicationException("It looks like an entity is missing in the DbContext. Maybe you forget to add a DbSet property?");
                    }
                }
            }

            return entity;
        }

        public IList GetCollectionNavigationPropertyValue<TEntity, TProperty>(TEntity entity, Type entityPropertyType)
            where TEntity : class
        {
            List<object> collectionItems = GetCollectionNavigationPropertyItems(entity, entityPropertyType);

            IList typedList = null;
            if (collectionItems != null && collectionItems.Any())
            {
                var listType = typeof(List<>);
                Type genericListType = listType.MakeGenericType(entityPropertyType);
                typedList = (IList)Activator.CreateInstance(genericListType);

                foreach (TProperty item in collectionItems)
                {
                    typedList.Add(item);
                }
            }

            return typedList;
        }

        public object GetNavigationPropertyValue<TEntity>(TEntity entity, PropertyInfo navigationProperty)
            where TEntity : class
        {
            object foreignNavigationProperty = GetForeignNavigationProperty(navigationProperty, entity);

            return foreignNavigationProperty;
        }

        private Type GetNavigationPropertyType(bool isCollection, PropertyInfo propertyInfo)
        {
            Type entityPropertyType;

            if (isCollection)
            {
                entityPropertyType = propertyInfo.PropertyType.GetGenericArguments()[0];
            }
            else
            {
                entityPropertyType = propertyInfo.PropertyType;
            }

            return entityPropertyType;
        }

        private List<dynamic> GetEntities(Type type)
        {
            if (Entities.ContainsKey(type))
            {
                return Entities[type];
            }

            return null;
        }

        public object GetForeignNavigationProperty<T>(PropertyInfo propertyInfo, T entity) where T : class
        {
            string propertyEntityName = propertyInfo.Name;

            string foreignKeyIdPropertyName = $"{propertyEntityName}Id";

            var foreignKeyProperty = entity.GetType().GetProperty(foreignKeyIdPropertyName);

            dynamic returnValue = null;
            if (foreignKeyProperty != null)
            {
                var foreignKeyId = Convert.ToInt32(foreignKeyProperty.GetValue(entity));

                if (foreignKeyId > 0)
                {
                    returnValue = GetEntities(propertyInfo.PropertyType).Single(x => x.GetType().GetProperty("Id").GetValue(x) == foreignKeyId);
                }
            }

            return returnValue;
        }

        public List<object> GetCollectionNavigationPropertyItems<TEntity>(TEntity entity, Type propertyType)
            where TEntity : class
        {
            Type entityType = entity.GetType();

            int id = (int)entityType.GetProperty("Id").GetValue(entity);
            List<object> returnValue = null;
            List<dynamic> entities = GetEntities(propertyType);

            if (entities != null && entities.Any())
            {
                string foreignKeyPropertyName = GetPropertyNameForType(propertyType, entityType);

                if (string.IsNullOrWhiteSpace(foreignKeyPropertyName))
                {
                    throw new ApplicationException(
                        $"Entity format invalid. Entity {entity.GetType().Name} is missing the foreign key properties.");
                }

                string foreignKeyIdPropertyName = $"{foreignKeyPropertyName}Id";

                returnValue = entities.Where(x => x.GetType().GetProperty(foreignKeyIdPropertyName).GetValue(x) == id).ToList();
            }

            return returnValue;
        }

        private string GetPropertyNameForType(Type entityType, Type propertyType)
        {
            PropertyInfo[] properties = entityType.GetProperties();
            string returnValue = null;

            foreach (PropertyInfo property in properties)
            {
                string propertyTypeName = property.PropertyType.Name;
                bool isCollection = IsCollectionTypeName(propertyTypeName);

                if (isCollection)
                {
                    continue;
                }

                string entityPropertyTypeName = GetNavigationPropertyType(false, property).Name;

                if (propertyType.Name.Equals(entityPropertyTypeName, StringComparison.InvariantCultureIgnoreCase))
                {
                    returnValue = property.Name;
                }
            }

            return returnValue;
        }

        private static bool IsCollectionTypeName(string propertyTypeName)
        {
            return propertyTypeName.Equals(typeof(ICollection<>).Name,
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}