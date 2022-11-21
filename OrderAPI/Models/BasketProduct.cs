using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace OrderAPI.Models
{
    public class BasketProduct
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Id продукта")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        [Display(Name = "Кол-во")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Id пользователя")]
        public int UserId { get; set; }
    }
}
