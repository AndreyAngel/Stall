using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace OrderAPI.Controllers
{
    [Route("ord/basketProduct")]
    public class BasketProductController : Controller
    {
        Context db;
        public BasketProductController(Context context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<JsonResult> Create(BasketProduct basketProduct)
        {
            Basket basket = await db.Baskets.FirstAsync(p => p.Id == basketProduct.UserId);

            bool flag = true;
            foreach (BasketProduct bp in basket.basketProductes)
            {
                if (bp.ProductId == basketProduct.ProductId)
                {
                    basketProduct.Quantity += bp.Quantity;
                    db.BasketProductes.Update(basketProduct);
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                await db.BasketProductes.AddAsync(basketProduct);
            }

            basket.ComputeTotalValue();
            db.Baskets.Update(basket);
            await db.SaveChangesAsync();
 
            return Json(basketProduct);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                BasketProduct basketProduct = new BasketProduct { Id = id.Value };
                db.Entry(basketProduct).State = EntityState.Deleted;

                Basket basket = await db.Baskets.FirstAsync(p => p.Id == basketProduct.UserId);
                basket.ComputeTotalValue();

                await db.SaveChangesAsync();
            }
            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<JsonResult> Update(BasketProduct basketProduct)
        {
            db.BasketProductes.Update(basketProduct);

            Basket basket = await db.Baskets.FirstAsync(p => p.Id == basketProduct.UserId);
            basket.ComputeTotalValue();

            await db.SaveChangesAsync();
            return Json(basketProduct);
        }
    }
}
