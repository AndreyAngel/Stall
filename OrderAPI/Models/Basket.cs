using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

namespace OrderAPI.Models
{
    public class Basket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; } // соответствует Id пользователя

        [Display(Name = "Список товаров")]
        public List<BasketProduct> basketProductes { get; set; }

        [Display(Name = "Общая стоимость")]
        public double TotalValue { get; set; } = 0;

        public Basket()
        {
            basketProductes = new List<BasketProduct>();
        }

        public void ComputeTotalValue()
        {
            TotalValue = basketProductes.Sum(p => p.Product.Price * p.Quantity);

        }

        public void Clear()
        {
            basketProductes.Clear();
            TotalValue = 0;
        }
    }
}
