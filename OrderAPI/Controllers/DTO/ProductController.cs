using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OrderAPI.Models;

namespace OrderAPI.Controllers.DTO
{
    [Route("ord/product")]
    public class ProductController : Controller
    {
        Context db;
        public ProductController(Context context)
        {
            db = context;
        }

        [HttpPost]
        public async void Create(Product product)
        {
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
        }

        [HttpPut]
        public async void Update(Product product)
        {
            db.Products.Update(product);
            await db.SaveChangesAsync();
        }

        [HttpDelete]
        public async void Delete(Product product)
        {
            db.Entry(product).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }
    }
}
