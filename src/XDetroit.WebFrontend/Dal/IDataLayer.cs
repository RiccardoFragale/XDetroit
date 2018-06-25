using System.Collections.Generic;
using XDetroit.WebFrontend.Models;

namespace XDetroit.WebFrontend.Dal
{
    public interface IDataLayer
    {
        BehaviourResult<ICollection<ItemCategory>> GetCategories(int pageSize, int pageNumber);
    }
}