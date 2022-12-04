using System.ComponentModel.DataAnnotations;

namespace OrderAPI.Models
{
    // It's product from storage
    public class StorageProduct
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Id продукта")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        [Display(Name = "Кол-во")]
        public int Quantity { get; set; }
    }
}
