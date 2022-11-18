using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Продукт")]
        public string Title { get; set; }

        [Display(Name = "Категория")]
        public string Category { get; set; }

        [Display(Name = "Бренд")]
        public string Brand { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public double Price { get; set; }
    }
}
