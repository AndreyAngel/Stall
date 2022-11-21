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

        [HttpGet]
        public async Task<JsonResult> Get(int user_id)
        {
            return Json(await db.Baskets.FirstOrDefaultAsync(p => p.UserId == user_id));
        }

        [HttpPut]
        public async Task<JsonResult> Clear(int user_id)
        {
            Basket basket = await db.Baskets.FirstOrDefaultAsync(p => p.UserId == user_id);
            basket.Clear();

            db.Baskets.Update(basket);
            await db.SaveChangesAsync();

            return Json(basket);
        }
    }
}
