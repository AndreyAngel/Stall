using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace OrderAPI.Controllers
{
    public class BasketProductController : Controller
    {
        Context db;
        public BasketProductController(Context context)
        {
            db = context;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<JsonResult> Get(int id)
        {
            return Json(db.BasketProductes.Where(p => p.Id == id));
        }

        [HttpPost]
        public async Task<JsonResult> Create(BasketProduct basketProduct)
        {
            Basket basket = await db.Baskets.FirstAsync(p => p.UserId == basketProduct.UserId);

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
