using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OrderAPI.Models;
using System.Collections.Generic;
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
            List<Stored_Product> st_prds = new List<Stored_Product>(); 
            bool flag = true;
            foreach (BasketProduct basketProduct in order.basketProducts)
            {
                Stored_Product st_pr = await db.Stored_Products.FirstOrDefaultAsync(p => p.ProductId == basketProduct.ProductId);
                if (basketProduct.Quantity > st_pr.Quantity)
                {
                    flag = false;
                    st_prds.Add(st_pr);
                }
            }
            if (flag)
            {
                await db.Orders.AddAsync(order);
                await db.SaveChangesAsync();
                return Json(order);
            }
            return Json($"Товаров нет в наличии: {st_prds}");
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
