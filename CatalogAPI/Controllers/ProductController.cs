using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CatalogAPI.Models;

namespace CatalogAPI.Controllers
{
    [Route("cat/product")]
    public class ProductController : Controller
    {

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

            Change change = new Change() { ProductId = product.Id, Status = "Created" };
            await db.Changes.AddAsync(change);

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

                Change change = new Change() { ProductId = product.Id, Status = "Deleted" };
                await db.Changes.AddAsync(change);

                await db.SaveChangesAsync();
            }
            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<JsonResult> Update(Product product)
        {
            Change change = new Change() { ProductId = product.Id, Status = "Updated" };
            await db.Changes.AddAsync(change);

            db.Products.Update(product);
            await db.SaveChangesAsync();
            return Json(product);
        }
    }
}
