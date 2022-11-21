using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CatalogAPI.Models;

namespace CatalogAPI.Controllers
{
    [Route("cat/category")]
    public class CategoryController : Controller
    {
        Context db;
        public CategoryController(Context context)
        {
            db = context;
        }


        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return Json(await db.Categories.Include(p => p.Products).ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<JsonResult> Get(int id)
        {
            Category category = await db.Categories.FirstOrDefaultAsync(p => p.Id == id);
            await db.Entry(category).Collection(p => p.Products).LoadAsync();
            return Json(category);
        }

        [HttpPost]
        public async Task<JsonResult> Create(Category category)
        {
            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();
            return Json(category);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Category category = new Category { Id = id.Value };
                db.Entry(category).State = EntityState.Deleted;
                await db.SaveChangesAsync();
            }
            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<JsonResult> Update(Category category)
        {
            db.Categories.Update(category);
            await db.SaveChangesAsync();
            return Json(category);
        }
    }
}
