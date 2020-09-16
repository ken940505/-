using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LLWP_Core.Models;
using LLWP_Core.Utility;
using LLWP_Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace LLWP_Core.Controllers
{
    public class ActivityBackController : Controller
    {
        private readonly dbLLWPContext _db;
        private readonly IHostEnvironment hostingEnvironment;
        public ActivityBackController(dbLLWPContext db, IHostEnvironment environment)
        {
            _db = db;
            hostingEnvironment = environment;
        }
        public IActionResult Index()
        {
            ActivityVM ad = new ActivityVM()
            {
                tActivitydata = _db.TActivitydata.ToList(),
                tActivityJoindata = _db.TActivityJoindata.ToList()
                //.Where(m => m.fJoinAcPeopleid == 1)
            };
            return View(ad);
        }

        public IActionResult Edit(int? id)
        {
            if (HttpContext.Session.GetObject<TMemberdata>(CDictionary.SK_LOGINED_CUSTOMER) == null)
                return Json(Url.Action("LogIn", "Members"));
            if (id == null)
                return RedirectToAction("List");
            TActivitydata a = _db.TActivitydata.FirstOrDefault(m => m.FActivityId == id);
            var b = new ActivityVM
            {
                activitydata = a
            };
            return View(b);
        }

        [HttpPost]
        public IActionResult Edit(ActivityVM p)
        {
            if (string.IsNullOrEmpty(p.activitydata.FActivityName))
                return RedirectToAction("List");

            TActivitydata ae = _db.TActivitydata.FirstOrDefault(m => m.FActivityId == p.activitydata.FActivityId);

            if (ae != null)
            {
                //照片檔案上傳，有新檔案要上傳(p.fImage!=null)才執行，否則會有例外錯誤
                if (p.activitydata.FActivityImages != null && p.actpic != null)
                {
                    string photName = Guid.NewGuid().ToString() + Path.GetExtension(p.actpic.FileName);
                    var uploads = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot/upImage");
                    var path = Path.Combine(uploads, photName);
                    p.actpic.CopyTo(new FileStream(path, FileMode.Create));
                    p.activitydata.FActivityImages = "/upImage/" + photName;
                    _db.TActivitydata.Add(p.activitydata);
                    //db.SaveChanges();
                }
                ae.FActivityCode = p.activitydata.FActivityCode;
                ae.FActivityName = p.activitydata.FActivityName;
                ae.FActivityTime = p.activitydata.FActivityTime;
                ae.FActivityLocation = p.activitydata.FActivityLocation;
                ae.FActivitypeopleLimit = p.activitydata.FActivitypeopleLimit;
                ae.FActivityJoinpeople = p.activitydata.FActivityJoinpeople;
                ae.FActivityPrice = p.activitydata.FActivityPrice;
                ae.FActivityCheck = p.activitydata.FActivityCheck;
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            TActivitydata ta = _db.TActivitydata.FirstOrDefault(m => m.FActivityId == id);
            if (ta != null)
            {
                _db.TActivitydata.Remove(ta);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //活動搜尋
        //public JsonResult search(string txtkeyword, string txtcheck)
        //{

        //    IQueryable<tActivitydata> products = null;
        //    string check = "";

        //    if (txtcheck == "true")
        //    {
        //        check = txtcheck;

        //        products = from a in (new dbLLWPEntities1()).tActivitydata
        //                   where a.fActivityCheck.Equals("否") && a.fActivityName.Contains(txtkeyword)
        //                   select a;
        //        return Json(products, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        products = from a in (new dbLLWPEntities1()).tActivitydata
        //                   where a.fActivityCheck.Equals("是") && a.fActivityName.Contains(txtkeyword)
        //                   select a;
        //        return Json(products, JsonRequestBehavior.AllowGet);
        //    }
        //}
        [HttpPost]
        public IActionResult Index(TActivitydata t)
        {
            if (HttpContext.Session.GetObject<TMemberdata>(CDictionary.SK_LOGINED_CUSTOMER) == null)
                return RedirectToAction("LogIn", "Members");

            string keyword = Request.Form["txtkeyword"];
            string txtmember = Request.Form["txtmember"];

            if (string.IsNullOrEmpty(keyword) && txtmember == null)
            {
                ActivityVM ad = new ActivityVM()
                {
                    tActivitydata = _db.TActivitydata.ToList(),
                    tActivityJoindata = _db.TActivityJoindata.ToList()
                    //.Where(m => m.fJoinAcPeopleid == 1)
                };
                return View(ad);
                //products = from p in (new dbLLWPEntities1()).tActivitydata
                //           select p;
            }
            else if (txtmember != null)
            {
                ActivityVM ad = new ActivityVM()
                {
                    tActivitydata = _db.TActivitydata
                    .Where(m => m.FActivityCheck == "否" && m.FActivityName.Contains(keyword)).ToList(),
                    tActivityJoindata = _db.TActivityJoindata.ToList()
                };
                return View(ad);
                //products = from p in (new dbLLWPEntities1()).tActivitydata
                //           where p.fActivityName.Contains(keyword) && p.fActivityName.Contains("是")
                //           select p;
            }
            else
            {
                ActivityVM ad = new ActivityVM()
                {
                    tActivitydata = _db.TActivitydata
                    .Where(m => m.FActivityName.Contains(keyword)).ToList(),
                    tActivityJoindata = _db.TActivityJoindata.ToList()
                };
                return View(ad);
            }


        }
    }
}
