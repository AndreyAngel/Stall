using System.Net.Http;
using System.Net.Http.Json;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    public class HttpClientController: ControllerBase
    {
        HttpClient client;
        public HttpClientController()
        {
            client = new HttpClient();
        }

        public async void createRequest(Product product)
        {
            JsonContent content = JsonContent.Create(product);
            await client.PostAsync("http://localhost:56302/ord/product", content);
        }

        public async void updateRequest(Product product)
        {
            JsonContent content = JsonContent.Create(product);
            await client.PutAsync("http://localhost:56302/ord/product", content);
        }

        public async void deleteRequest(int id)
        {
            await client.DeleteAsync($"http://localhost:56302/ord/product/{id}");
        }
    }
}