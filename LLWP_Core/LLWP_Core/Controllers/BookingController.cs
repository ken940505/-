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

        public void DateIntoSession(string InYear, string InMonth, string InDate, string OutYear, string OutMonth, string OutDate)
        {
            string[] InYearArray = InYear.Split(' ');
            string[] InMonthArray = InMonth.Split(' ');
            string[] InDateArray = InDate.Split(' ');
            string[] OutYearArray = OutYear.Split(' ');
            string[] OutMonthArray = OutMonth.Split(' ');
            string[] OutDateArray = OutDate.Split(' ');

            InYear = InYearArray[0];
            InMonth = InMonthArray[1];
            InDate = InDateArray[1];
            OutYear = OutYearArray[0];
            OutMonth = OutMonthArray[1];
            OutDate = OutDateArray[1];


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
