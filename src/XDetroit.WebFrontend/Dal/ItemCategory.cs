using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XDetroit.WebFrontend.Dal
{
    public class ItemCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<ProductItem> ColProducts { get; set; }
    }
}