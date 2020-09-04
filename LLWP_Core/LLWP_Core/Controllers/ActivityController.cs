using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LLWP_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace LLWP_Core.Controllers
{
    public class ActivityController : Controller
    {
        private readonly dbLLWPContext _db;
        private readonly IHostEnvironment hostingEnvironment;

        public ActivityController(dbLLWPContext db, IHostEnvironment environment)
        {
            _db = db;
            hostingEnvironment = environment;
        }

        public IActionResult Index()
        {
            //var activity = from a in (new dbLLWPEntities1()).tActivitydata
            //                select a;
            IQueryable<TActivitydata> products = null;
            //string keyword = Request.Form["txtkeyword"];
            string keyword = "";
            if (string.IsNullOrEmpty(keyword))
            {
                products = from p in _db.TActivitydata
                           select p;
            }
            else
            {
                products = from p in _db.TActivitydata
                           where p.FActivityName.Contains(keyword)
                           select p;
            }
            return View(products);
        }

        [HttpPost]
        public ActionResult Index(TActivitydata t)
        {
            string keyword = Request.Form["txtkeyword"];
            if (keyword != null)
            {
                IQueryable<TActivitydata> products = null;

                if (string.IsNullOrEmpty(keyword))
                {
                    products = from p in _db.TActivitydata
                               select p;
                }
                else
                {
                    products = from p in _db.TActivitydata
                               where p.FActivityName.Contains(keyword)
                               select p;
                }
                return View(products);
            }
            else
            {
                var ta = new TActivitydata();
                ta.FActivityCode = "A020";
                ta.FActivityName = t.FActivityName;
                ta.FActivityTime = t.FActivityTime;
                ta.FActivitypeopleLimit = t.FActivitypeopleLimit;
                ta.FActivityPrice = t.FActivityPrice;
                ta.FActivityLocation = t.FActivityLocation;
                ta.FActivityCheck = "否";
                //if (t.Equals != null)
                //{
                //    string photoname = Guid.NewGuid().ToString() + Path.GetExtension(t.actpic.FileName);
                //    var path = Path.Combine(Server.MapPath("~/Content/images"), photoname);
                //    t.actpic.SaveAs(path);
                //    ta.fActivityImages = "../Content/images/" + photoname;
                //}
                _db.TActivitydata.Add(ta);
                _db.SaveChanges();

                return RedirectToAction("index");
            }
            //    tActivitydata ta = new tActivitydata();
            //    ta.fActivityCode = "A020";
            //    ta.fActivityName = t.fActivityName;
            //    ta.fActivityTime = t.fActivityTime;
            //    ta.fActivitypeopleLimit = t.fActivitypeopleLimit;
            //    ta.fActivityPrice = t.fActivityPrice;
            //    ta.fActivityLocation = t.fActivityLocation;
            //    ta.fActivityCheck = "否";
            //    if (t.actpic != null)
            //    {
            //        string photoname = Guid.NewGuid().ToString() + Path.GetExtension(t.actpic.FileName);
            //        var path = Path.Combine(Server.MapPath("~/Content/images"), photoname);
            //        t.actpic.SaveAs(path);
            //        ta.fActivityImages = "../Content/images/" + photoname;
            //    }
            //    dbLLWPEntities1 db = new dbLLWPEntities1();
            //    db.tActivitydata.Add(ta);
            //    db.SaveChanges();

            //return RedirectToAction("index");
        }
        public IActionResult Inside()
        {
            return View();
        }

        public IActionResult club()
        {
            return View();
        }

        public IActionResult plantclub()
        {
            return View();
        }
        //public string actaddcart(int[] id)
        //{
        //    dbLLWPEntities1 db = new dbLLWPEntities1();
        //    tActivitydata ta = db.tActivitydata.FirstOrDefault(m => m.fActivityId == id[0]);
        //    string aa = $"<div>12345<div>";
        //    return aa;
        //}
        //結帳頁面
        public IActionResult actshoppingcart()
        {
            return View();
        }

        public string actinside(int? id)
        {
            TActivitydata ta = _db.TActivitydata.FirstOrDefault(m => m.FActivityId == id);

            string aa = $"<div id='main' class='show'>" +
                $"<div id='content'><div class='content'><div class='contentphoto'><img src='{ta.FActivityImages}'></div><div class='contenttitle'>" +
                $"<span class='condate'><span class='datemonth'>7月</span><span class='dateday'>23號</span></span><div class='cttitle'>{ta.FActivityName}" +
                $"</div><div class='cthost'>{ta.FActivityLocation}</div>" +
                $"<div class='ctdatetime'>{ta.FActivityTime}</div><div class='hosthttp'>主辦方網站<a href='http://localhost:8080/plantclub.html'>http://localhost:8080/plantclub.html</a>" +
                $"</div></div></div><div class='details'><div class='detailinside'><h4 id='test1'>詳情" +
                $"<div class='fz125'>想為居家環境增添綠意嗎？這時不妨動手做出玩偶般的草頭寶寶，為植物妝點各種表情，更顯得生動活潑，還能依照心情更換打扮。照顧方式很簡單，只要每天給水就可以生存，草葉枯成黃色還能變化造型，是十分簡單的擺飾品。</div>" +
                $"</h4></div></div><div class='Participants'>" +
                $"<div class='Participantsinside'><h4 id='test2'>參加人員</h4></div></div></div></div>";

            return aa;
        }

        public string actaddshoppingcart(int? id)
        {
            TActivitydata ta = _db.TActivitydata.FirstOrDefault(m => m.FActivityId == id);
            string bb = $"<tr id='{ta.FActivityId}' value='{ta.FActivityId}' class='selectshopping shoppingcart-tr' style='text-align:center;height:300px'><td>" +
                       $"<img src='{ta.FActivityImages}' style='height:200px;width:200px;'>" +
                       $"<div>{ta.FActivityName}</div></td>" +
                       $"<td>{ta.FActivityTime}</td>" +
                       $"<td>{ta.FActivityPrice}</td>" +
                       $"<td id='delact'><button class='activity_Index_btn delbtn btn btn-danger' onclick='delbtn({ta.FActivityId})' id='{ta.FActivityId}'>取消</button></td></tr>";

            return bb;
        }
    }
}
