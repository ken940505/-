using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LLWP_Core.Models;

namespace LLWP_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly dbLLWPContext _db;

        public HomeController(dbLLWPContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Access()
        {
            return View();
        }

        public IActionResult CoffeeTop()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Environment()
        {
            return View();
        }

        public IActionResult LongCover()
        {
            return View();
        }        

        public IActionResult QandA()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
