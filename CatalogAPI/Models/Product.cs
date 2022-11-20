using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Продукт")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public double Price { get; set; }


        [Display(Name = "Категория")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }


        [Display(Name = "Бренд")]
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
