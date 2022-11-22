using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OrderAPI.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Продукт")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public double Price { get; set; }
    }
}
