using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LLWP_Core.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult BookingCalendar()
        {
            return View();
        }
        
        public IActionResult BookingFirstPage()
        {
            return View();
        }

        public IActionResult BookingPayment()
        {
            return View();
        }

        public IActionResult BookingRoomSelect()
        {
            return View();
        }
    }
}
