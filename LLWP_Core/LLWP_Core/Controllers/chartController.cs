using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LLWP_Core.Controllers
{
    public class chartController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
