using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OrderAPI.Models;
using System.Collections.Generic;

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
        public async Task<IActionResult> Create(Order order)
        {
            List<StorageProduct> storageproducts = new List<StorageProduct>(); 
            bool flag = true;
            foreach (BasketProduct basketProduct in order.basketProducts)
            {
                StorageProduct storageproduct = await db.StorageProducts.FirstOrDefaultAsync(p => p.ProductId == basketProduct.ProductId);
                if (basketProduct.Quantity > storageproduct.Quantity)
                {
                    flag = false;
                    storageproducts.Add(storageproduct);
                }
            }
            if (flag)
            {
                await db.Orders.AddAsync(order);
                await db.SaveChangesAsync();
                return Json(order);
            }
            return NotFound($"Товаров нет в наличии: {storageproducts}");
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
                return Ok("Ok");
            }
            return NotFound();
        }
    }
}
