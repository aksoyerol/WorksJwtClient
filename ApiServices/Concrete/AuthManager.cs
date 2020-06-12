using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WorksJwtClient.ApiServices.Interfaces;
using WorksJwtClient.Models;

namespace WorksJwtClient.ApiServices.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IHttpContextAccessor _accessor;
        public AuthManager(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public async Task<bool> Login(AppUserLogin appUserLogin)
        {
            var jsonData = JsonConvert.SerializeObject(appUserLogin);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();

            var responseMessage = await httpClient.PostAsync("http://localhost:63846/Api/Auth/SignIn", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var token =  JsonConvert.DeserializeObject<AccessToken>(await responseMessage.Content.ReadAsStringAsync());
                _accessor.HttpContext.Session.SetString("token", token.Token);
                return true;
            }
            return false;


        }

        public void LogOut()
        {
           _accessor.HttpContext.Session.Remove("token");
        }
    }
}