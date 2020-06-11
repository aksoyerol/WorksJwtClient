using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WorksJwtClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(){
            List<string> isimler = new List<string>{"erol","aksoy","güçlü","selin","deneme","yanılma"};
          
            return View(isimler.ToList());
        }
    }
}