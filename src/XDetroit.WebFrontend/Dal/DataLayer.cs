using System;
using System.Collections.Generic;
using XDetroit.WebFrontend.Models;

namespace XDetroit.WebFrontend.Dal
{
    public class DataLayer : IDataLayer
    {
        public ICollection<ItemCategory> GetCategories(int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }
    }
}