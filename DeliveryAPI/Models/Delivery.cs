using DeliveryAPI.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace DeliveryAPI.Models
{
    public class Delivery
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Id заказа")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        [Display(Name = "Адрес доставки")]
        public string Address { get; set; }
    }
}
