using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using WorksJwtClient.Builders.Concrete;
using WorksJwtClient.Models;

namespace WorksJwtClient.CustomFilters
{
    public class JwtAuthorize : ActionFilterAttribute
    {
        public string Roles { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Session.GetString("token");

            if (!string.IsNullOrWhiteSpace(token))
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = httpClient.GetAsync("http://localhost:63846/Api/Auth/ActiveUser").Result;

                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var activeUser = JsonConvert.DeserializeObject<AppUser>(responseMessage.Content.ReadAsStringAsync().Result);

                    //Üç durum var [JwtAuthorize], [JwtAuthorize(Roles="Admin,Member.....)], [JwtAuthorize(Roles="Admin"]
                    JwtAuthorizeHelper.CheckUserRole(activeUser,Roles,context);
                }

                else if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    context.HttpContext.Session.Remove("token");
                    context.Result = new RedirectToActionResult("SignIn", "Account", null);
                }
                else
                {
                    context.HttpContext.Session.Remove("token");
                    var statusCode = responseMessage.StatusCode.ToString();
                    context.Result = new RedirectToActionResult("ApiError","Account",new {code=statusCode});

                }
            }
            else
            {
                context.Result = new RedirectToActionResult("SignIn", "Account", null);
            }
        }
    }
}