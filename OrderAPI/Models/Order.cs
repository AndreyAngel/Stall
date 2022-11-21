using System.ComponentModel.DataAnnotations;

namespace OrderAPI.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Id пользователя")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Id товара")]
        public int ProductId { get; set; }
    }
}
