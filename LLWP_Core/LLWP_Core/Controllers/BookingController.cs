using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using LLWP_Core.Models;
using LLWP_Core.Utility;
using LLWP_Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LLWP_Core.Controllers
{
    public class BookingController : Controller
    {
        private readonly dbLLWPContext _db;

        public BookingController(dbLLWPContext db)
        {
            _db = db;
        }

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
            InMonth = InMonthArray[1].Length == 1 ? "0" + InMonthArray[1] : InMonthArray[1];
            InDate = InDateArray[1].Length == 1 ? "0" + InDateArray[1] : InDateArray[1];
            OutYear = OutYearArray[0];
            OutMonth = OutMonthArray[1].Length == 1 ? "0" + OutMonthArray[1] : OutMonthArray[1];
            OutDate = OutDateArray[1].Length == 1 ? "0" + OutDateArray[1] : OutDateArray[1];

            var bookingData = new TOrTable 
            { 
                FOrCheckIn = string.Format("{0}-{1}-{2}",InYear,InMonth,InDate),
                FOrCheckOut = string.Format("{0}-{1}-{2}",OutYear,OutMonth,OutDate)
            };

            HttpContext.Session.SetObject(CDictionary.SK_BOOKINGDATA, bookingData);
        }

        public IActionResult BookingRoomSelect()
        {
            var bookingData = HttpContext.Session.GetObject<TOrTable>(CDictionary.SK_BOOKINGDATA);

            if (bookingData == null)
                return RedirectToAction(nameof(BookingCalendar));

            var activityData = _db.TActivitydata.Where(o => string.Compare(o.FActivityTime,bookingData.FOrCheckIn) >= 0 && 
                                                            string.Compare(o.FActivityTime,bookingData.FOrCheckOut) <= 0).ToList();

            var tryPetData = _db.TTryPetTable.OrderBy(p => p.FTryPetId).ToList();

            var list = new List<string>();
            var tryPetListSelectNumber = new List<string>();
            for (int i = 0; i < tryPetData.Count; i++)
            {
                list.Add((100+i).ToString());
            }
            for (int i = 0; i < 3; i++)
            {
                Random rand = new Random();
                var randNumber = rand.Next(0, list.Count - i);
                tryPetListSelectNumber.Add(list[randNumber]);
                list.RemoveAt(randNumber);
            }

            var tryPetDataSelect = _db.TTryPetTable.Where(p => p.FTryPetNum == tryPetListSelectNumber[0] || 
                                                          p.FTryPetNum == tryPetListSelectNumber[1] || 
                                                          p.FTryPetNum == tryPetListSelectNumber[2]);

            var activityAndtryPet = new BookingVM
            {
                activitydata = activityData,
                tryPetTable = tryPetDataSelect.ToList()
            };

            return View(activityAndtryPet);
        }

        public IActionResult BookingPayment()
        {
            return View();
        }

        public IActionResult BookingFirstPage()
        {

            return View();
        }
    }
}
