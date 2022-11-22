using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OrderAPI.Models;
using System.Linq;

namespace OrderAPI.Controllers
{
    [Route("ord/basket")]
    public class BasketController : Controller
    {
        Context db;
        public BasketController(Context context)
        {
            db = context;
        }

        // Receiving the basket (all productes)
        [HttpPost]
        public async Task<JsonResult> Get(int user_id)
        {
            return Json(await db.Baskets.Include(p => p.basketProductes).FirstOrDefaultAsync(p => p.Id == user_id));
        }


        // Method called when user registers
        [HttpPost]
        public async Task<JsonResult> Create(Basket basket)
        {
            await db.Baskets.AddAsync(basket);
            await db.SaveChangesAsync();
            return Json(basket);
        }

        // Emptying the basket 
        [HttpPut]
        public async Task<JsonResult> Clear(int user_id)
        {
            Basket basket = await db.Baskets.Include(p => p.basketProductes).FirstOrDefaultAsync(p => p.Id == user_id);
            basket.Clear();

            db.Baskets.Update(basket);
            await db.SaveChangesAsync();

            return Json(basket);
        }
    }
}
