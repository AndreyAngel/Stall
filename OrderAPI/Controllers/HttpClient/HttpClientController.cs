using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using OrderAPI.Models;
using Newtonsoft.Json;
using OrderAPI.Controllers.DTO;

namespace OrderAPI.Controllers
{
    public class HttpClientController : Controller
    {
        HttpClient client;
        public HttpClientController()
        {
            using var client = new HttpClient();
        }
        public async void Get_changes()
        {
            string response = await client.GetStringAsync("http://localhost:56302/cat/product");
            List<Change> changes = JsonConvert.DeserializeObject<List<Change>>(response);
        }
    }
}
