﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OrderAPI.Models;
using System.Net.Http;

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
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id != null)
            {
                Basket basket = await db.Baskets.FirstOrDefaultAsync(p => p.Id == id);
                await db.Entry(basket).Collection(p => p.basketProductes).LoadAsync();
                return Json(basket);
            }
            return NotFound();

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
        [Route("{id:int}")]
        public async Task<JsonResult> Clear(int id)
        {
            Basket basket = await db.Baskets.Include(p => p.basketProductes).FirstOrDefaultAsync(p => p.Id == id);
            basket.Clear();

            db.Baskets.Update(basket);
            await db.SaveChangesAsync();

            return Json(basket);
        }

        public async void Get_changes()
        {
            using var client = new HttpClient();
            var responce = await client.GetAsync("http://localhost:XXXXX/cat/product");
        }
    }
}
