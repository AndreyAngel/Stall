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
            return Json(await db.Products.Include(p => p.Category).
                                          Include(p => p.Brand).
                                          ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<JsonResult> Get(int id)
        {
            return Json(await db.Products.Include(p => p.Category).
                                          Include(p => p.Brand).
                                          FirstOrDefaultAsync(p => p.Id == id));
        }

        [HttpPost]
        public async Task<JsonResult> Create(Product product)
        {
            await db.Products.AddAsync(product);
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
            }
            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<JsonResult> Update(Product product)
        {
            db.Products.Update(product);
            await db.SaveChangesAsync();
            return Json(product);
        }
    }
}
