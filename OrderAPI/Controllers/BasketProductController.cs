using Microsoft.AspNetCore.Mvc;
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
            Basket basket = await db.Baskets.Include(p => p.basketProducts).FirstAsync(p => p.Id == basketProduct.BasketId);
            Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == basketProduct.ProductId);
            basketProduct.TotalValue = basketProduct.Quantity * product.Price;

            bool flag = true;
            foreach (BasketProduct bp in basket.basketProducts)
            {
                if (bp.ProductId == basketProduct.ProductId)
                {
                    bp.Quantity += basketProduct.Quantity;
                    bp.TotalValue += basketProduct.TotalValue;
                    db.BasketProducts.Update(bp);
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                await db.BasketProducts.AddAsync(basketProduct);
            }
            await db.SaveChangesAsync();

            basket = await db.Baskets.Include(p => p.basketProducts).FirstAsync(p => p.Id == basketProduct.BasketId);
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

                Basket basket = await db.Baskets.FirstAsync(p => p.Id == basketProduct.BasketId);
                basket.ComputeTotalValue();

                await db.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<JsonResult> Update(BasketProduct basketProduct)
        {
            db.BasketProducts.Update(basketProduct);

            Basket basket = await db.Baskets.FirstAsync(p => p.Id == basketProduct.BasketId);
            basket.ComputeTotalValue();

            await db.SaveChangesAsync();
            return Json(basketProduct);
        }
    }
}
