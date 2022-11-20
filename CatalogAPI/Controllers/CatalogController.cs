using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CatalogAPI.Models;
using System.Collections.Generic;

namespace CatalogAPI.Controllers
{
    [Route("cat/catalog")]
    public class CatalogController : Controller
    {
        Context db;
        public CatalogController(Context context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var brands = Json(await db.Brands.ToListAsync());
            var categories = Json(await db.Categories.ToArrayAsync());

            var response = new Dictionary<string, object>()
            {
                {"Brands", brands.Value},
                {"Categories", categories.Value}
            };

            return Json(response);
        }
    }
}
