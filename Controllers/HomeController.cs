using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorksJwtClient.ApiServices.Interfaces;
using WorksJwtClient.CustomFilters;
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

        [JwtAuthorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Index()
        {
            return View(await _productApiService.GetAllAsync());
        }

        [JwtAuthorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [JwtAuthorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductAdd productAdd)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.AddAsync(productAdd);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Something else wrong !");

            return View(productAdd);
        }

        [JwtAuthorize(Roles="Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _productApiService.GetByIdAsync(id));
        }

        [JwtAuthorize(Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(ProductList productList)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(productList);
                return RedirectToAction("Index");
            }
            return View(productList);
        }

        [JwtAuthorize(Roles="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productApiService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}