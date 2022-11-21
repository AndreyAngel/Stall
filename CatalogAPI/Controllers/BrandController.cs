using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CatalogAPI.Models;

namespace CatalogAPI.Controllers
{
    [Route("cat/brand")]
    public class BrandController : Controller
    {
        Context db;
        public BrandController(Context context)
        {
            db = context;
        }


        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return Json(await db.Brands.Include(p => p.Products).ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<JsonResult> Get(int id)
        {
            Brand brand = await db.Brands.FirstOrDefaultAsync(p => p.Id == id);
            await db.Entry(brand).Collection(p => p.Products).LoadAsync();
            return Json(brand);
        }

        [HttpPost]
        public async Task<JsonResult> Create(Brand brand)
        {
            await db.Brands.AddAsync(brand);
            await db.SaveChangesAsync();
            return Json(brand);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Brand brand = new Brand { Id = id.Value };
                db.Entry(brand).State = EntityState.Deleted;
                await db.SaveChangesAsync();
            }
            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<JsonResult> Update(Brand brand)
        {
            db.Brands.Update(brand);
            await db.SaveChangesAsync();
            return Json(brand);
        }
    }
}
