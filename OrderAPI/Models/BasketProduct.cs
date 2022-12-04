using System.ComponentModel.DataAnnotations;

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


        [Display(Name = "Общая стоимость")]
        public double TotalValue { get; set; } = 0;

        [Required]
        [Display(Name = "Id карзины")]
        public int BasketId { get; set; }
        public Basket Basket { get; set; }

        public void ComputeTotalValue(Product product)
        {
            TotalValue = Quantity * product.Price;

        }
    }
}
