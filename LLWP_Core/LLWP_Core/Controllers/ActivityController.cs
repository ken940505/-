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
            if (HttpContext.Session.GetObject<TMemberdata>(CDictionary.SK_LOGINED_CUSTOMER) == null)
                return RedirectToAction("LogIn", "Members");

            ActivityVM activityVM = new ActivityVM()
            {
                tActivitydata = _db.TActivitydata.ToList(),
                tActivityJoindata = _db.TActivityJoindata.Where(m => m.FJoinAcPeopleid == 1).ToList()
            };

            return View(activityVM);
        }

        [HttpPost]
        public IActionResult Index(ActivityVM t)
        {
            t.activitydata.FActivityCode = "A020";
            t.activitydata.FActivityCheck = "否";
            t.activitydata.FActivityJoinpeople = 0;

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
        public ActionResult club()
        {
            return View();
        }
        public ActionResult plantclub()
        {
            return View();
        }
        //結帳頁面活動
        //public string ordershoppingcart(int? id)
        //{
        //    var ta = _db.TActivitydata.FirstOrDefault(m => m.FActivityId == id);

        //     string aa = $"<div class='card ml3 mb3' style='width:18rem;'>" +
        //        $"<div class='card-body'>" +
        //        $"<h6 class='card-subtitle mb-2 text-muted fz125'>{ta.FActivityName}</h6>" +
        //        $"<p class='card-text'>" +
        //        $"<div>日期:{ta.FActivityTime}</div>" +
        //        $"<div>價格$ <span class='allprice'>{Convert.ToInt32(ta.FActivityPrice)}</span></div>" +
        //        $"<div>地點:{ta.FActivityLocation}</div>" +
        //        $"</p></div></div>";

        //    return aa;
        //}

        public string ordershoppingcart(int? id)
        {
            var ta = _db.TActivitydata.FirstOrDefault(m => m.FActivityId == id);

            var money = "";
            if (ta.FActivityPrice == 0)
                money = $"<div> 費用：免費參加 </div>";
            else
                money = $"<div>費用：<span class='allprice'>{Convert.ToInt32(ta.FActivityPrice)}</span> 元</div>";

            string aa = $"<div class='card mb3 ml-4 mr-4' style='width:18rem;'>" +
               $"<img src = {ta.FActivityImages} class='card-img-top'>" +
               $"<div class='card-body'>" +
               $"<h6 class='card-subtitle mb-2 text-muted fz125'>{ta.FActivityName}</h6>" +
               $"<p class='card-text'>" +
               $"<div>日期：{ta.FActivityTime}</div>" + 
               money +
               $"<div>地點：{ta.FActivityLocation}</div>" +
               $"</p></div></div>";

            return aa;
        }

        //結帳後存入資料庫
        public string shoppingcartendpay(int? id)
        {
            HttpContext.Session.SetObject(CDictionary.SK_Payment, true);
            var memberDate = HttpContext.Session.GetObject<TMemberdata>(CDictionary.SK_LOGINED_CUSTOMER);
            if (id != null)
            {
                var ta = _db.TActivitydata.FirstOrDefault(m => m.FActivityId == id);
                var tj = new TActivityJoindata
                {
                    JoinAcid = ta.FActivityId,
                    JoinAcCode = ta.FActivityCode,
                    FJoinAcPeopleid = 1
                };
                var memactivity = new TMemActivity
                {
                    FAcId = ta.FActivityId,
                    FAcMeId = memberDate.FMeId
                };

                _db.TActivityJoindata.Add(tj);
                _db.TMemActivity.Add(memactivity);
                ta.FActivityJoinpeople += 1;
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
            string joinname = "";
            foreach (var item in _db.TActivityJoindata)
            {
                if (item.JoinAcid == id)
                {
                    TMemberdata tm = _db.TMemberdata.FirstOrDefault(m => m.FMeId == item.FJoinAcPeopleid);
                    joinname += tm.FMeName + "/";
                }
            }
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
                $"<div class='Participantsinside'><h4 id='test2'>參加人員</h4>{joinname}</div></div></div></div></div>";
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
            IQueryable<TActivitydata> products = null;
            var productsList = new List<TActivitydata>();
            int pricemin = 0;
            int pricemax = 9999;
            string keyword = "";
            string member = "";
            if (txtkeyword == null)
                txtkeyword = "";

            if (txtpricemin != null)
                pricemin = Convert.ToInt32(txtpricemin);

            if (txtpricemax != null)
                pricemax = Convert.ToInt32(txtpricemax);

            if (txtpricemax != null)
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

                productsList = products.ToList();

                return Json(productsList);
            }
            else
            {
                products = from a in _db.TActivitydata
                           where a.FActivityCheck.Equals("是") 
                              && a.FActivityName.Contains(txtkeyword) 
                              && pricemin <= a.FActivityPrice 
                              && a.FActivityPrice <= pricemax
                           select a;

                productsList = products.ToList();

                return Json(productsList);
            }
        }
    }
}
