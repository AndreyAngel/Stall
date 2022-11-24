using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DeliveryAPI.Models;
using System.Net.Http;

namespace DeliveryAPI.Controllers
{
    [Route("del/delivery")]
    public class DeliveryController : Controller
    {
        Context db;
        public DeliveryController(Context context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return Json(await db.Deliveries.ToListAsync());
        }

        [HttpGet]
        [Route("id:int")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id != null)
                return Json(await db.Deliveries.FirstOrDefaultAsync(p => p.Id == id));
            return NotFound();
        }

        [HttpPost]
        public async Task<JsonResult> Create(Delivery delivery)
        {
            await db.Deliveries.AddAsync(delivery);
            await db.SaveChangesAsync();
            return Json(delivery);
        }

        [HttpDelete]
        [Route("id:int")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Delivery delivery = new Delivery { Id = id.Value };
                db.Entry(delivery).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return Json("OK");
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<JsonResult> Update(Delivery delivery)
        {
            db.Deliveries.Update(delivery);
            await db.SaveChangesAsync();
            return Json(delivery);
        }
    }
}
