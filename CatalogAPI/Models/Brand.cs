using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CatalogAPI.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название бренда")]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
        public Brand()
        {
            Products = new List<Product>();
        }
    }
}
