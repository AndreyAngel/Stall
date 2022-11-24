using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace OrderAPI.Models
{
    public class Stored_Product
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
