using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace OrderAPI.Controllers
{
    [Route("ord/storage")]
    public class StorageController : Controller
    {
        Context db;
        public StorageController(Context context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return Json(await db.StorageProducts.ToListAsync());
        }

        [HttpGet]
        [Route("id:int")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id != null)
                return Json(await db.StorageProducts.FirstOrDefaultAsync(p => p.ProductId == id));
            return NotFound();
        }

        [HttpPost]
        public async Task<JsonResult> Create(StorageProduct StorageProduct)
        {
            StorageProduct storageproduct = await db.StorageProducts.FirstOrDefaultAsync(p => p.ProductId == StorageProduct.ProductId);
            if (storageproduct == null)
            {
                await db.StorageProducts.AddAsync(StorageProduct);
                await db.SaveChangesAsync();
                return Json(StorageProduct);
            }
            storageproduct.Quantity += StorageProduct.Quantity;
            db.StorageProducts.Update(storageproduct);
            await db.SaveChangesAsync();
            return Json(storageproduct);

        }

        [HttpPut]
        public async Task<JsonResult> Update(StorageProduct StorageProduct)
        {
            db.StorageProducts.Update(StorageProduct);
            await db.SaveChangesAsync();
            return Json(StorageProduct);
        }

        [HttpDelete]
        [Route("id:int")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                StorageProduct StorageProduct = new StorageProduct { ProductId = id.Value };
                db.Entry(StorageProduct).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return Ok("Ok");
            }
            return NotFound();
        }

    }
}
