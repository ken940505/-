using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using LLWP_Core.Models;
using LLWP_Core.Utility;
using LLWP_Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Packaging;

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

        public IActionResult DateIntoSession(string InYear, string InMonth, string InDate, string OutYear, string OutMonth, string OutDate)
        {
            if (HttpContext.Session.GetObject<TMemberdata>(CDictionary.SK_LOGINED_CUSTOMER) == null)
                return Json(Url.Action("LogIn", "Members"));

            if ((InYear == null) || (InMonth == null) || (InDate == null) || (OutYear == null) || (OutMonth == null) || (OutDate == null))
                return Json(Url.Action("BookingCalendar", "Booking"));

            var bookingData = new TOrTable
            {
                FOrCheckIn = SD.DateToString(InYear, InMonth, InDate,"in"),
                FOrCheckOut = SD.DateToString(OutYear, OutMonth, OutDate, "out"),
                FOrday = (Convert.ToDateTime(SD.DateToString(OutYear, OutMonth, OutDate, "out")) - Convert.ToDateTime(SD.DateToString(InYear, InMonth, InDate,"in"))).Days
            };

            HttpContext.Session.SetObject(CDictionary.SK_BOOKINGDATA, bookingData);
            return Json(Url.Action("BookingRoomSelect", "Booking"));
        }

        public IActionResult BookingRoomSelect()
        {
            if (HttpContext.Session.GetObject<TMemberdata>(CDictionary.SK_LOGINED_CUSTOMER) == null)
                return Json(Url.Action("LogIn", "Members"));

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

        [HttpPost]
        public IActionResult BookingRoomSelect(int peopleNum, int takePet, int joinTryPet, int roomType, int?[] addActivity, int?[] addActivity1, int?[] addActivity2, int[]? petradio)
        {
            TMemberdata memberdata = HttpContext.Session.GetObject<TMemberdata>(CDictionary.SK_LOGINED_CUSTOMER);

            if (memberdata == null)
                return Json(Url.Action("LogIn", "Members"));

            TOrTable bookingData = HttpContext.Session.GetObject<TOrTable>(CDictionary.SK_BOOKINGDATA);

            bookingData.FOrNum = SD.fOrNumRandomId();
            bookingData.FOrDate = SD.DateTimeNow();
            bookingData.FOrGuestOneId = memberdata.FMeId;
            bookingData.FOrPeople = peopleNum;
            bookingData.FOrTryPetId = petradio[0];

            if (joinTryPet == 1)
                bookingData.FOrTryPet = "Y";

            var totalMoney = 0M;

            if (peopleNum == 1)
            {
                bookingData.FOrGuestOneActivityA = SD.FillingArray(addActivity)[0];
                bookingData.FOrGuestOneActivityB = SD.FillingArray(addActivity)[1];
                bookingData.FOrGuestOneActivityC = SD.FillingArray(addActivity)[2];
                for (int i = 0; i < SD.FillingArray(addActivity).Length; i++)
                {
                    totalMoney += _db.TActivitydata.FirstOrDefault(id => id.FActivityId == SD.FillingArray(addActivity)[i]).FActivityPrice;
                }
            }
            else
            {
                bookingData.FOrGuestOneActivityA = SD.FillingArray(addActivity1)[0];
                bookingData.FOrGuestOneActivityB = SD.FillingArray(addActivity1)[1];
                bookingData.FOrGuestOneActivityC = SD.FillingArray(addActivity1)[2];
                bookingData.FOrGuestTwoActivityA = SD.FillingArray(addActivity2)[0];
                bookingData.FOrGuestTwoActivityB = SD.FillingArray(addActivity2)[1];
                bookingData.FOrGuestTwoActivityC = SD.FillingArray(addActivity2)[2];
                for (int i = 0; i < addActivity1.Length; i++)
                {
                    totalMoney += _db.TActivitydata.FirstOrDefault(id => id.FActivityId == addActivity1[i]).FActivityPrice;
                }

                for (int i = 0; i < addActivity2.Length; i++)
                {
                    totalMoney += _db.TActivitydata.FirstOrDefault(id => id.FActivityId == addActivity2[i]).FActivityPrice;
                }
            }

            bookingData.FOrTotalPrice = totalMoney + SD.DaysMoney(bookingData.FOrday, roomType);

            HttpContext.Session.SetObject(CDictionary.SK_BOOKINGDATA, bookingData);

            return RedirectToAction(nameof(BookingPayment));
        }

        public IActionResult RefreshPet()
        {
            var tryPetData = _db.TTryPetTable.OrderBy(p => p.FTryPetId).ToList();

            var list = new List<string>();
            var tryPetListSelectNumber = new List<string>();
            for (int i = 0; i < tryPetData.Count; i++)
            {
                list.Add((100 + i).ToString());
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
                                                          p.FTryPetNum == tryPetListSelectNumber[2]).ToList();

            return Json(tryPetDataSelect);
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
