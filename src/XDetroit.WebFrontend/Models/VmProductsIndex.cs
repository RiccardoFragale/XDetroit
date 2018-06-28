using System.Collections.Generic;
using XDetroit.WebFrontend.Dal;

namespace XDetroit.WebFrontend.Models
{
    public class VmProductsIndex
    {
        public IEnumerable<ProductItem> Products { get; set; }
    }
}