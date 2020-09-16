using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LLWP_Core.Models;
using LLWP_Core.Utility;
using Microsoft.AspNetCore.Mvc;

namespace LLWP_Core.Controllers
{
    public class calendarController : Controller
    {
        private readonly dbLLWPContext _db;
        public calendarController(dbLLWPContext db)
        {
            _db = db;
        }

        class test
        {
            public string inRoom { get; set; }
            public string outRoom { get; set; }
            public string roomNum { get; set; }
        }

        class days
        {
            public string date { get; set; }
            public int count { get; set; }
            public IEnumerable<test> roomNum { get; set; }
        }

        public IActionResult CalendarList()
        {
            if (HttpContext.Session.GetObject<TMemberdata>(CDictionary.SK_LOGINED_CUSTOMER) == null)
                return RedirectToAction("LogIn", "Members");
            return View();
        }

        public JsonResult GetEvents_emptyRoom()
        {
            List<string> dateList = new List<string>();
            string month = "";
            string day = "";
            for (var j = 7; j <= 12; j++)
            {
                if (j == 7 || j == 8 || j == 10 || j == 12)
                {
                    //大月
                    if (j < 10)
                        month = $"0{j.ToString()}";
                    else
                        month = $"{j.ToString()}";

                    for (var i = 1; i <= 31; i++)
                    {
                        if (i < 10)
                            day = $"0{i.ToString()}";
                        else
                            day = $"{i.ToString()}";
                        dateList.Add($"2020-{month}-{day}");
                    }
                }
                else
                {
                    //小月
                    if (j < 10)
                        month = $"0{j.ToString()}";
                    else
                        month = $"{j.ToString()}";

                    for (var i = 1; i <= 30; i++)
                    {
                        if (i < 10)
                            day = $"0{i.ToString()}";
                        else
                            day = $"{i.ToString()}";
                        dateList.Add($"2020-{month}-{day}");
                    }
                }
            }

            List<days> Order = new List<days>();

            //var events = from o in db.tOrTable
            //              from r in db.tRmTable
            //              where !(o.fOrRoomId == r.fRmID) && string.Compare(o.fOrCheckIn, "2020-09-02") == 0 && (r.fRmNum.StartsWith("A2") || r.fRmNum.StartsWith("B2"))
            //              select r;

            for (int i = 0; i < dateList.Count; i++)
            {
                var allday = dateList[i];
                var events = _db.TOrTable.Join(_db.TRmTable, o => o.FOrRoomId, r => r.FRmId, (o, r) =>
                                new test { inRoom = o.FOrCheckIn, outRoom = o.FOrCheckOut, roomNum = r.FRmNum })
                               .Where(t => string.Compare(allday, t.inRoom) >= 0 && string.Compare(allday, t.outRoom) <= 0 && (t.roomNum.StartsWith("A2") || t.roomNum.StartsWith("B2")));

                var roomNum = events.OrderBy(o => o.roomNum).ToList();

                //空房暴力算法:count= 24 - events.Count()
                Order.Add(new days { date = allday, roomNum = roomNum, count = 24 - events.Count() });
            }

            //return Json( events, JsonRequestBehavior.AllowGet );
            return Json(Order);

        }
    }
}
