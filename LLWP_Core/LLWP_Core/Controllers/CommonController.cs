using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LLWP_Core.Models;
using LLWP_Core.Utility;
using LLWP_Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LLWP_Core.Controllers
{
    public class CommonController : Controller
    {
        private readonly dbLLWPContext _db;
        public CommonController(dbLLWPContext db)
        {
            _db = db;
        }
        public IActionResult Home()
        {
            //if (Session[cSessionDictionaryLogin.SK_LOGEDIN_USER] == null)
            //    return RedirectToAction("Login");
            //return RedirectToAction("ChartList", "chart");
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Clogin p)
        {
            // CURTIS9348@gmail.com
            // 078XiBV9#qno
            TMemberdata user = _db.TMemberdata.FirstOrDefault(m => m.FMeMail == p.txtAccount && m.FMePass == p.txtPassword);
            //user為null表示帳密比對錯誤
            if (user == null)
                return View();

            HttpContext.Session.SetObject(CDictionary.SK_LOGINED_CUSTOMER, user);
            HttpContext.Session.SetObject(CDictionary.SK_CUSTOMERNAME, user.FMeName);

            return RedirectToAction("Home");
        }
    }
}
