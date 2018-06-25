using System.Collections.Generic;
using XDetroit.WebFrontend.Models;

namespace XDetroit.WebFrontend.Dal
{
    public interface IDataLayer
    {
        ICollection<ItemCategory> GetCategories(int pageSize, int pageNumber);
    }
}