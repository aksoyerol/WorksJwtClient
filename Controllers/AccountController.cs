using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorksJwtClient.ApiServices.Interfaces;
using WorksJwtClient.Models;

namespace WorksJwtClient.Controllers{
    public class AccountController : Controller{

        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult SignIn(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLogin appUserLogin){
            if(ModelState.IsValid){
                if(await _authService.Login(appUserLogin) == true){
                    
                }
                ModelState.AddModelError("","Username or pass wrong !");

            }
            return View(appUserLogin);
        }

    }
}