using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LLWP_Core.Models;
using LLWP_Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using LLWP_Core.Utility;

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
            var activity = _db.TActivitydata.OrderBy(a => a.FActivityId);

            return View(activity);
        }

        [HttpPost]
        public IActionResult Index(ActivityVM t)
        {
            ActivityVM ta = t;
            ta.activitydata.FActivityCode = "A020";
            ta.activitydata.FActivityCheck = "否";
            ta.activitydata.FActivityJoinpeople = 0;

            if (t.actpic != null)
            {
                string photName = Guid.NewGuid().ToString() + Path.GetExtension(t.actpic.FileName);
                var uploads = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot/upImage");
                var path = Path.Combine(uploads, photName);
                t.actpic.CopyTo(new FileStream(path, FileMode.Create));
                t.activitydata.FActivityImages = "/" + photName;
            }

            _db.TActivitydata.Add(t.activitydata);
            _db.SaveChanges();

            return RedirectToAction("index");
        }
        //結帳頁面活動
        public string ordershoppingcart(int? id)
        {
            var ta = _db.TActivitydata.FirstOrDefault(m => m.FActivityId == id);

            string aa = $"<div class='card ml3 mb3' style='width:18rem;'>" +
                $"<div class='card-body'>" +
                $"<h6 class='card-subtitle mb-2 text-muted fz125'>{ta.FActivityName}</h6>" +
                $"<p class='card-text'>" +
                $"<div>日期:{ta.FActivityTime}</div>" +
                $"<div>價格$ <span class='allprice'>{Convert.ToInt32(ta.FActivityPrice)}</span></div>" +
                $"<div>地點:{ta.FActivityLocation}</div>" +
                $"</p></div></div>";

            return aa;
        }
        //結帳後存入資料庫
        public string shoppingcartendpay(int? id)
        {
            if (id != null)
            {
                var ta = _db.TActivitydata.FirstOrDefault(m => m.FActivityId == id);
                var tj = new TActivityJoindata();
                tj.JoinAcid = ta.FActivityId;
                tj.JoinAcCode = ta.FActivityCode;
                tj.FJoinAcPeopleid = 1;
                _db.TActivityJoindata.Add(tj);
                _db.SaveChanges();
            }
            string a = "";
            return a;
        }
        public ActionResult actshoppingcart()
        {
            return View();
        }

        //活動詳細
        public string actinside(int? id)
        {
            var ta = _db.TActivitydata.FirstOrDefault(m => m.FActivityId == id);
            string mon = ta.FActivityTime.Substring(5, 2);
            string day = ta.FActivityTime.Substring(8, 2);
            string sec = ta.FActivityTime.Substring(10);
            string aa = $"<div id='main' class='show'>" +
                $"<div id='content'><div class='content'><div class='contentphoto'><img src='{ta.FActivityImages}'></div><div class='contenttitle'>" +
                $"<span class='condate'><span class='datemonth'>{mon}月</span><span class='dateday'>{day}號</span><span>{sec}</span></span>" +
                $"<div class='cttitle'>{ta.FActivityName}</div><div class='cthost fz1'>{ta.FActivityLocation}</div>" +
                $"<div class='hosthttp' style='margin-left:20%;'>主辦方網站<a href='http://localhost:8080/plantclub.html'>http://localhost:8080/plantclub.html</a>" +
                $"<div><i class='fas fa-users fz1 mr3'>限制人次:{ta.FActivitypeopleLimit}</i></div>" +
                $"<div><i class='fas fa-user-check fz1 mr3'>已報名人次:{ta.FActivityJoinpeople}</i></div>" +
                $"<div class='fz1'>${Convert.ToInt32(ta.FActivityPrice)}</div>" +
                $"</div></div></div><div class='details'><div class='detailinside'><h4 id='test1'>詳情" +
                $"<div class='fz125'>想為居家環境增添綠意嗎？這時不妨動手做出玩偶般的草頭寶寶，為植物妝點各種表情，更顯得生動活潑，還能依照心情更換打扮。照顧方式很簡單，只要每天給水就可以生存，草葉枯成黃色還能變化造型，是十分簡單的擺飾品。</div>" +
                $"</h4></div></div><div class='Participants'>" +
                $"<div class='Participantsinside'><h4 id='test2'>參加人員</h4></div></div></div></div></div>";
            return aa;
        }
        //購物車加入項目
        public string actaddshoppingcart(int? id)
        {
            var ta = _db.TActivitydata.FirstOrDefault(m => m.FActivityId == id);
            string bb = $"<tr id='{ta.FActivityId}' value='{ta.FActivityId}' class='selectshopping shoppingcart-tr Checkout' style='text-align:center;height:300px'><td>" +
                       $"<img src='{ta.FActivityImages}' style='height:200px;width:200px;'>" +
                       $"<div>{ta.FActivityName}</div></td>" +
                       $"<td>{ta.FActivityTime}</td>" +
                       $"<td>{Convert.ToInt32(ta.FActivityPrice)}</td>" +
                       $"<td id='delact'><button class='activity_Index_btn delbtn btn btn-danger' onclick='delbtn({ta.FActivityId})' id='{ta.FActivityId}'>取消</button></td></tr>";

            return bb;
        }
        //活動搜尋
        public IActionResult search(string txtkeyword, string txtpricemin, string txtpricemax, string txtmember)
        {
            var list = new List<TActivitydata>();

            HttpContext.Session.SetObject(CDictionary.SK_ADDINTOACTIVITYCART, list);

            IQueryable<TActivitydata> products = null;

            int pricemin = 0;
            int pricemax = 9999;
            string keyword = "";
            string member = "";
            if (txtpricemin != "")
                pricemin = Convert.ToInt32(txtpricemin);

            if (txtpricemax != "")
                pricemax = Convert.ToInt32(txtpricemax);

            if (txtpricemax != "")
                keyword = txtpricemax;

            if (txtmember == "true")
            {
                member = txtmember;

                products = from a in _db.TActivitydata
                           where a.FActivityCheck.Equals("是")
                              && a.FActivityName.Contains(txtkeyword)
                              && pricemin <= a.FActivityPrice
                              && a.FActivityPrice <= pricemax
                              && (a.FActivitypeopleLimit - a.FActivityJoinpeople) > 0
                           select a;

                return Json(products);
            }
            else
            {
                products = from a in _db.TActivitydata
                           where a.FActivityCheck.Equals("是") 
                              && a.FActivityName.Contains(txtkeyword) 
                              && pricemin <= a.FActivityPrice 
                              && a.FActivityPrice <= pricemax
                           select a;

                return Json(products);
            }
        }
    }
}
