using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace OrderAPI.Models
{
    public class Basket
    {
        public int Id { get; set; }

        [Display(Name = "Id пользователя")]
        public int UserId { get; set; }

        [Display(Name = "Список товаров")]
        public List<BasketProduct> basketProductes { get; set; }

        [Display(Name = "Общая стоимость")]
        public double TotalValue { get; set; }

        public Basket()
        {
            basketProductes = new List<BasketProduct>();
        }

        public decimal ComputeTotalValue()
        {
            return (decimal)basketProductes.Sum(p => p.Product.Price * p.Quantity);

        }
        public void Clear()
        {
            basketProductes.Clear();
        }
    }
}
