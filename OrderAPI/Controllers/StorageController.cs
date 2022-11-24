using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return Json(await db.Stored_Products.ToListAsync());
        }

        [HttpGet]
        [Route("id:int")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id != null)
                return Json(await db.Stored_Products.FirstOrDefaultAsync(p => p.ProductId == id));
            return NotFound();
        }

        [HttpPost]
        public async Task<JsonResult> Create(Stored_Product stored_Product)
        {
            Stored_Product st_pr = await db.Stored_Products.FirstOrDefaultAsync(p => p.ProductId == stored_Product.ProductId);
            if (st_pr == null)
            {
                await db.Stored_Products.AddAsync(stored_Product);
                await db.SaveChangesAsync();
                return Json(stored_Product);
            }
            st_pr.Quantity += stored_Product.Quantity;
            db.Stored_Products.Update(st_pr);
            await db.SaveChangesAsync();
            return Json(st_pr);

        }

        [HttpPut]
        public async Task<JsonResult> Update(Stored_Product stored_Product)
        {
            db.Stored_Products.Update(stored_Product);
            await db.SaveChangesAsync();
            return Json(stored_Product);
        }

        [HttpDelete]
        [Route("id:int")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Stored_Product stored_Product = new Stored_Product { ProductId = id.Value };
                db.Entry(stored_Product).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return Json("OK");
            }
            return NotFound();
        }

    }
}
