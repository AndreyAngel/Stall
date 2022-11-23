using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OrderAPI.Models;
using System.Linq;

namespace OrderAPI.Controllers
{
    [Route("ord/order")]
    public class OrderController : Controller
    {
        Context db;
        public OrderController(Context context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return Json(await db.Orders.ToListAsync());
        }

        [HttpGet]
        [Route("id:int")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id != null)
                return Json(await db.Orders.FirstOrDefaultAsync(p => p.Id == id));
            return NotFound();
        }

        [HttpPost]
        public async Task<JsonResult> Create(Order order)
        {
            await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();
            return Json(order);
        }

        [HttpPut]
        public async Task<JsonResult> Update(Order order)
        {
            db.Orders.Update(order);
            await db.SaveChangesAsync();
            return Json(order);
        }

        [HttpDelete]
        [Route("id:int")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Order order = new Order { Id = id.Value };
                db.Entry(order).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return Json("OK");
            }
            return NotFound();
        }
    }
}
