using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CatalogAPI.Models;

namespace CatalogAPI.Controllers
{
    public class ChangeController : Controller
    {
        Context db;
        public ChangeController(Context context)
        {
            db = context;
        }

        public async Task<JsonResult> Get()
        {
            return Json(await db.Changes.ToListAsync());
        }
    }
}
