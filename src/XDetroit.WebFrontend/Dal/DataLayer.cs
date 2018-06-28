using System;
using System.Collections.Generic;
using System.Linq;
using XDetroit.WebFrontend.Models;

namespace XDetroit.WebFrontend.Dal
{
    public class DataLayer : IDataLayer
    {
        private readonly IDataProvider dataProvider;

        public DataLayer(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public BehaviourResult<ICollection<ItemCategory>> GetCategories(int pageSize, int pageNumber)
        {
            int skipCount = pageSize * pageNumber;
            var returnValue = new BehaviourResult<ICollection<ItemCategory>>
            {
                Value = dataProvider.GetEntities<ItemCategory>().OrderBy(x=>x.Id).Skip(skipCount).Take(pageSize).ToList()
            };

            return returnValue;
        }

        public BehaviourResult<ItemCategory> CreateCategory(ItemCategory model)
        {
            var returnValue = new BehaviourResult<ItemCategory>
            {
                Value = dataProvider.CreateEntity(model)
            };

            dataProvider.SaveChanges();

            return returnValue;
        }

        public BehaviourResult<ICollection<ProductItem>> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}