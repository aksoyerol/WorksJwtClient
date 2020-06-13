using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WorksJwtClient.ApiServices.Interfaces;
using WorksJwtClient.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace WorksJwtClient.ApiServices.Concrete
{
    public class ProductApiManager : IProductApiService
    {
        private readonly IHttpContextAccessor _accessor;
        public ProductApiManager(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public async Task AddAsync(ProductAdd productAdd)
        {
            var activeToken = _accessor.HttpContext.Session.GetString("token");
            if (!string.IsNullOrWhiteSpace(activeToken))
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", activeToken);
                var jsonData = JsonConvert.SerializeObject(productAdd);
                StringContent sendingData = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await httpClient.PostAsync("http://localhost:63846/api/products", sendingData);

            }
        }

        public async Task DeleteAsync(int id)
        {
            var activeToken = _accessor.HttpContext.Session.GetString("token");
            if(!string.IsNullOrWhiteSpace(activeToken)){
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",activeToken);
                await httpClient.DeleteAsync($"http://localhost:63846/api/products/{id}");
            }
        }

        public async Task<List<ProductList>> GetAllAsync()
        {
            var activeToken = _accessor.HttpContext.Session.GetString("token");
            if (!string.IsNullOrWhiteSpace(activeToken))
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", activeToken);
                var responseMessage = await httpClient.GetAsync("http://localhost:63846/api/products");
                if (responseMessage.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<ProductList>>(await responseMessage.Content.ReadAsStringAsync());
                }
            }
            return null;
        }

        public async Task<ProductList> GetByIdAsync(int id)
        {
            var activeToken = _accessor.HttpContext.Session.GetString("token");
            if (!string.IsNullOrWhiteSpace(activeToken))
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", activeToken);
                var responseMessage = await httpClient.GetAsync($"http://localhost:63846/api/products/{id}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var product = JsonConvert.DeserializeObject<ProductList>(await responseMessage.Content.ReadAsStringAsync());

                    return product;
                }
            }
            return null;
        }

        public async Task UpdateAsync(ProductList productList)
        {
            var activeToken = _accessor.HttpContext.Session.GetString("token");
            if(!string.IsNullOrWhiteSpace(activeToken)){
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",activeToken);
                var jsonData = JsonConvert.SerializeObject(productList);
                var stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");
                var responseMessage = await httpClient.PutAsync("http://localhost:63846/api/products",stringContent);
               
            }
        }
    }
}