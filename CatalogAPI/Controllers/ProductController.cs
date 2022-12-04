using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CatalogAPI.Models;

namespace CatalogAPI.Controllers
{
    [Route("cat/product")]
    public class ProductController : Controller
    {
        HttpClientController client;
        Context db;
        public ProductController(Context context)
        {
            db = context;
        }


        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return Json(await db.Products.ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<JsonResult> Get(int id)
        {
            Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
            await db.Entry(product).Reference(p => p.Category).LoadAsync();
            await db.Entry(product).Reference(p => p.Brand).LoadAsync();
            return Json(product);
        }

        [HttpPost]
        public async Task<JsonResult> Create(Product product)
        {
            await db.Products.AddAsync(product);

            client = new HttpClientController();
            client.createRequest(product);

            await db.SaveChangesAsync();
            return Json(product);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Product product = new Product { Id = id.Value };
                db.Entry(product).State = EntityState.Deleted;
                await db.SaveChangesAsync();

                client = new HttpClientController();
                client.deleteRequest(id.Value);

                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<JsonResult> Update(Product product)
        {
            client = new HttpClientController();
            client.updateRequest(product);

            db.Products.Update(product);
            await db.SaveChangesAsync();

            return Json(product);
        }
    }
}
