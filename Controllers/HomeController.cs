using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorksJwtClient.ApiServices.Interfaces;
using WorksJwtClient.Models;

namespace WorksJwtClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductApiService _productApiService;

        public HomeController(IProductApiService productApiService)
        {
            _productApiService = productApiService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _productApiService.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductAdd productAdd)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.AddAsync(productAdd);
                return RedirectToAction("Index","Home");
            }
            ModelState.AddModelError("","Something else wrong !");

            return View(productAdd);
        }

        public async Task<IActionResult> Edit(int id){
            return View(await _productApiService.GetByIdAsync(id));
        }
    }
}