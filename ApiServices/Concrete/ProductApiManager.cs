using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WorksJwtClient.ApiServices.Interfaces;
using WorksJwtClient.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WorksJwtClient.ApiServices.Concrete{
    public class ProductApiManager : IProductApiService
    {
        private readonly IHttpContextAccessor _accessor;
        public ProductApiManager(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
    
        public async Task<List<ProductList>> GetAllAsync()
        {
            var activeToken = _accessor.HttpContext.Session.GetString("token");
            if(!string.IsNullOrWhiteSpace(activeToken)){
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",activeToken);
                var responseMessage = await httpClient.GetAsync("http://localhost:63846/api/products");
                if(responseMessage.IsSuccessStatusCode){
                    return JsonConvert.DeserializeObject<List<ProductList>>(await responseMessage.Content.ReadAsStringAsync());
                }
            }
            return null;
        }
    }
}