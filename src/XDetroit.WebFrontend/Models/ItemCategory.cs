using System.ComponentModel.DataAnnotations;

namespace XDetroit.WebFrontend.Models
{
    public class ItemCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}