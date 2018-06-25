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
            var returnValue = new BehaviourResult<ICollection<ItemCategory>>
            {
                Value = dataProvider.GetEntities<ItemCategory>().ToList()
        };

            return returnValue;
        }
    }
}