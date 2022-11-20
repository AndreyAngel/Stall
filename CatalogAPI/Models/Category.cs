using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CatalogAPI.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название категории")]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }
}
