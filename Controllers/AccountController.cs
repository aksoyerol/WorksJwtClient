using Microsoft.AspNetCore.Mvc;

namespace WorksJwtClient.Controllers{
    public class AccountController : Controller{

        public IActionResult SignIn(){
            return View();
        }

    }
}