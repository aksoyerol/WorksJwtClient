using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorksJwtClient.ApiServices.Interfaces;

namespace WorksJwtClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductApiService _productApiService;

        public HomeController(IProductApiService productApiService)
        {
            _productApiService = productApiService;
        }
        public async Task<IActionResult> Index(){
            return View(await _productApiService.GetAllAsync());
        }
    }
}