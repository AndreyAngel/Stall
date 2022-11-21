using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace OrderAPI.Models
{
    public class Brand
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название бренда")]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
        public Brand()
        {
            Products = new List<Product>();
        }
    }
}
