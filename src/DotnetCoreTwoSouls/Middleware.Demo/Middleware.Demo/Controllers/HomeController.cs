using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Middleware.Demo.Models;

namespace Middleware.Demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Privacy()
        {
            // 故意在拜訪 Privacy View 噴例外...
            throw new ApplicationException("Can't visit privacy view.");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
