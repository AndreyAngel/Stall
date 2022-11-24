using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace OrderAPI.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Id пользователя")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Товары")]
        public List<BasketProduct> basketProducts { get; set; }

        [Display(Name = "Статус")]
        public bool Status { get; set; } = false;   // false - готовится, true - готов

        [Display(Name = "Статус оплаты")]
        public bool Payment_State { get; set; } = false;

        [Display(Name = "Дата и время заказа")]
        public static DateTime DateTime { get; set; } = DateTime.Now;

        public Order()
        {
            basketProducts = new List<BasketProduct>();
        }
    }
}
