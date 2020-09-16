using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LLWP_Core.Models;
using LLWP_Core.Utility;
using Microsoft.AspNetCore.Mvc;

namespace LLWP_Core.Controllers
{
    public class ChartController : Controller
    {
        private readonly dbLLWPContext _db;
        public ChartController(dbLLWPContext db)
        {
            _db = db;
        }

        public IActionResult ChartList()
        {
            if (HttpContext.Session.GetObject<TMemberdata>(CDictionary.SK_LOGINED_CUSTOMER) == null)
                return RedirectToAction("LogIn", "Members");

            return View();
        }

        public IActionResult GetPieChartData()
        {
            //失敗，moth6Data count=0
            //var month6Data = from o in db.tOrTable
            //                 where (o.fOrDate.CompareTo("2020/06/01") >= 0 && o.fOrDate.CompareTo("2020/06/30") <= 0)
            //                 select o;
            //ratio.month6 = month6Data.Count();

            var month6DataTest = _db.TOrTable.Where(d => string.Compare(d.FOrDate, "2020-06-01") >= 0 && string.Compare(d.FOrDate, "2020-06-30") <= 0).Count();
            var month7DataTest = _db.TOrTable.Where(d => string.Compare(d.FOrDate, "2020-07-01") >= 0 && string.Compare(d.FOrDate, "2020-07-31") <= 0).Count();
            var month8DataTest = _db.TOrTable.Where(d => string.Compare(d.FOrDate, "2020-08-01") >= 0 && string.Compare(d.FOrDate, "2020-08-31") <= 0).Count();
            var month9DataTest = _db.TOrTable.Where(d => string.Compare(d.FOrDate, "2020-09-01") >= 0 && string.Compare(d.FOrDate, "2020-09-30") <= 0).Count();

            monthRatio ratio = new monthRatio();
            ratio.month6 = month6DataTest;
            ratio.month7 = month7DataTest;
            ratio.month8 = month8DataTest;
            ratio.month9 = month9DataTest;

            return Json(ratio);
        }
        public class monthRatio
        {
            public int month6 { get; set; }
            public int month7 { get; set; }
            public int month8 { get; set; }
            public int month9 { get; set; }
        }
        public IActionResult GetLineChartData()
        {
            var test = _db.TOrTable.GroupBy(o => o.FOrDate.Substring(0, 7)).Select(g => new { fOrDateMonth = g.Key, sumTotalPrice = g.Sum(m => m.FOrTotalPrice) });

            //var result = from s in db.tOrTable
            //             group s by new { date = new DateTime(System.Convert.ToDateTime(s.fOrDate).Year, System.Convert.ToDateTime(s.fOrDate).Month, 1) } into g
            //             select  new (g => new
            //             {
            //                 fOrDate = g.Key.date,
            //                 sumTotalPrice = g.Sum(x => x.fOrTotalPrice)
            //             });

            //var test = db.tOrTable.GroupBy(o => o.fOrDate.Substring(0, 7));

            //var result = db.tOrTable
            //             .GroupBy ( g=> date =new DateTime(System.Convert.ToDateTime(g.fOrDate).Year, System.Convert.ToDateTime(g.fOrDate).Month, 1) )  
            //             .Select (p => new{fOrDate = p.Key.date,sumTotalPrice = p.Sum(x => x.fOrTotalPrice)}).ToList();

            //date = new DateTime(System.Convert.ToDateTime(g.fOrDate).Year, System.Convert.ToDateTime(g.fOrDate).Month, 1)
            //var result = db.tOrTable
            //            .GroupBy(g => )
            //            .Select(p => new { fOrDate = p.Key.date, sumTotalPrice = p.Sum(x => x.fOrTotalPrice) }).ToList();


            return Json(test);
        }

        public IActionResult test()
        {
            var test = _db.TOrTable.GroupBy(o => o.FOrDate.Substring(0, 7)).Select(g => new { name = g.Key, count = g.Sum(m => m.FOrTotalPrice) });
            return Json(test);
        }
    }
}
