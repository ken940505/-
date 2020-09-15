using System;
using System.Linq;
using System.Net.Mail;
using LLWP_Core.Utility;
using Microsoft.AspNetCore.Mvc;
using LLWP_Core.Models;
using LLWP_Core.ViewModels;
using System.IO;
using Microsoft.Extensions.Hosting;
using Stripe.BillingPortal;

namespace LLWP_Core.Controllers
{
    public class MembersController : Controller
    {
        private readonly dbLLWPContext _db;
        private readonly IHostEnvironment hostingEnvironment;

        public MembersController(dbLLWPContext db, IHostEnvironment environment)
        {
            _db = db;
            hostingEnvironment = environment;
        }

        public IActionResult LogIn()
        {
            if (HttpContext.Session.GetObject<TMemberdata>(CDictionary.SK_LOGINED_CUSTOMER) != null)
                return RedirectToAction(nameof(MemberProfile));

            var code = HttpContext.Session.GetObject<string>(CDictionary.SK_CODE);
            //string code = Session[Cdictionary.SK_CODE] as string;//驗證碼
            code = SD.CodeCreate(4);
            HttpContext.Session.SetObject(CDictionary.SK_CODE, code);
            ViewBag.CODE = code;

            return View();
        }

        [HttpPost]
        public IActionResult LogIn(Clogin p, string company)
        {
            var code = HttpContext.Session.GetObject<string>(CDictionary.SK_CODE);
            if (!code.Equals(p.txtCord) && company == null)
            {
                HttpContext.Session.SetObject("SK_AUTHERROR", "驗證碼錯誤，登入失敗");
                return RedirectToAction("Login");
            }

            string fEmail = (p.txtAccount);
            TMemberdata cust = _db.TMemberdata.FirstOrDefault(t => t.FMeMail == fEmail && t.FMePass.Equals(p.txtPassword));

            if (cust == null || p.txtAccount != cust.FMeMail || p.txtPassword != cust.FMePass)
            {
                HttpContext.Session.SetObject("SK_AUTHERROR", "帳密錯誤，登入失敗");
                return RedirectToAction("Login");
            }

            HttpContext.Session.SetObject(CDictionary.SK_LOGINED_CUSTOMER, cust);
            HttpContext.Session.SetObject(CDictionary.SK_CUSTOMERNAME, cust.FMeName);

            if (company != null)
                return RedirectToAction("ChartList", "Chart");
            else 
                return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            HttpContext.Session.Remove(CDictionary.SK_LOGINED_CUSTOMER);
            HttpContext.Session.Remove(CDictionary.SK_CUSTOMERNAME);
            return RedirectToAction("Index", "Home");
        }

        public string CreateCodeIn()
        {
            var code = SD.CodeCreate(4);
            HttpContext.Session.SetObject(CDictionary.SK_CODE, code);
            return code;
        }

        // GET: Members
        public IActionResult MemberProfile()
        {
            HttpContext.Session.Remove(CDictionary.SK_Payment);

            if (HttpContext.Session.GetObject<TMemberdata>(CDictionary.SK_LOGINED_CUSTOMER) == null)
                return RedirectToAction("Login");

            var memberDate = HttpContext.Session.GetObject<TMemberdata>(CDictionary.SK_LOGINED_CUSTOMER);

            var memberNumber = memberDate.FMeNumber;

            var petData = _db.TMempetdata.FirstOrDefault(p => p.FPeMemNumber == memberDate.FMeNumber);
            if (petData == null)
                petData = new TMempetdata();

            var tOrtable = _db.TOrTable.FirstOrDefault(o => o.FOrGuestOneId == memberDate.FMeId);
            if (tOrtable == null)
                tOrtable = new TOrTable();

            var memHealthdata = _db.TMemHealthdata.FirstOrDefault(h => h.FHeMemNumber == memberDate.FMeNumber);
            if (memHealthdata == null)
                memHealthdata = new TMemHealthdata();

            var dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            var memActivityJoin = _db.TMemActivity.Join(_db.TActivitydata, a => a.FAcId, d => d.FActivityId, (a, d) => 
                                      new activityJoin { activityId = a.FAid, memberId = a.FAcMeId, date = d.FActivityTime, name = d.FActivityName, location = d.FActivityLocation, image = d.FActivityImages })
                                     .Where(m=>m.memberId == memberDate.FMeId && (string.Compare(m.date, dateNow) == 1))
                                     .OrderBy(d=>d.date).ToList();
            if (memActivityJoin == null)
                memActivityJoin = memActivityJoin.Where(m => m.image == "0").ToList();


            ViewModelMP MPVM = new ViewModelMP();
            MPVM = new ViewModelMP { merberData = memberDate, petData = petData, orTable = tOrtable, memHealthdata = memHealthdata, activityJoin = memActivityJoin};

            return View(MPVM);
        }

        public IActionResult RegisteredPet()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisteredPet(TMempetdata p)
        {
            _db.TMempetdata.Add(p);
            _db.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult RegisteredLongTime(int? id)
        {
            var p = new ViewModelMP();
            if (id == null)
            {
                var TMemberdataidEqualZero = new TMemberdata { FMeId = 0 };
                var TMempetdataidEqualZero = new TMempetdata { FPeId = 0 };
                p = new ViewModelMP { merberData = TMemberdataidEqualZero, petData = TMempetdataidEqualZero };
                return View(p);
            }

            var memberdbdata = _db.TMemberdata.FirstOrDefault(o => o.FMeId == id);
            var memberNumber = memberdbdata.FMeNumber;

            if (memberdbdata == null)
                return NotFound();

            var petdbData = _db.TMempetdata.FirstOrDefault(o => o.FPeMemNumber == memberNumber);

            //var memberData = new TMemberdata
            //{
            //    FMeId = memberdbdata.FMeId,
            //    FMeNumber = memberdbdata.FMeNumber,
            //    FMeGender = memberdbdata.FMeGender,
            //    FMeName = memberdbdata.FMeName,
            //    FMeBirth = memberdbdata.FMeBirth,
            //    FMeMail = memberdbdata.FMeMail,
            //    FMePass = memberdbdata.FMePass,
            //    FMePhone = memberdbdata.FMePhone,
            //    FMeAge = memberdbdata.FMeAge,
            //    FMePersonId = memberdbdata.FMePersonId,
            //    FMePhoto = memberdbdata.FMePhoto,
            //    FMeEmerName = memberdbdata.FMeEmerName,
            //    FMeEmerPhone = memberdbdata.FMeEmerPhone
            //};

            if (petdbData != null)
            {
                //var petData = new TMempetdata
                //{
                //    FPeMemNumber = petdbData.FPeMemNumber,
                //    FPeVarity = petdbData.FPeVarity,
                //    FPeAge = petdbData.FPeAge,
                //    FPeWeight = petdbData.FPeWeight,
                //    FPeBirth = petdbData.FPeBirth,
                //    FPeSex = petdbData.FPeSex,
                //    FPeVac = petdbData.FPeVac,
                //    FPeFix = petdbData.FPeFix,
                //    FPePhoto = petdbData.FPePhoto
                //};

                p = new ViewModelMP
                {
                    merberData = memberdbdata,
                    petData = petdbData
                };
            }
            else
            {
                var TMempetdataidEqualZero = new TMempetdata { FPeId = 0 };
                p = new ViewModelMP { merberData = memberdbdata, petData = TMempetdataidEqualZero };
            }


            return View(p);
        }

        [HttpPost]
        public IActionResult RegisteredLongTime(ViewModelMP p)
        {
            string selectPet = Request.Form["selectPet"];


            if (!ModelState.IsValid)
                return View(p);

            if (p.merberData.FMeId == 0)
            {
                if (p.fPhotodata != null)
                {
                    string photName = Guid.NewGuid().ToString() + Path.GetExtension(p.fPhotodata.FileName);
                    var uploads = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot/upImage");
                    var path = Path.Combine(uploads, photName);
                    p.fPhotodata.CopyTo(new FileStream(path, FileMode.Create));
                    p.merberData.FMePhoto = "/" + photName;
                }

                var code = SD.CodeCreate(6);

                p.merberData.FMeNumber = code;
                p.petData.FPeMemNumber = code;
                p.petData.FPeMem = code;
                p.petData.FPeNumber = code;

                _db.TMemberdata.Add(p.merberData);

                if (selectPet == "1")
                {
                    if (p.fPePhotodata != null)
                    {
                        string photName = Guid.NewGuid().ToString() + Path.GetExtension(p.fPePhotodata.FileName);
                        var uploads = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot/upImage");
                        var path = Path.Combine(uploads, photName);
                        p.fPePhotodata.CopyTo(new FileStream(path, FileMode.Create));
                        p.petData.FPePhoto = "/" + photName;
                    }
                    _db.TMempetdata.Add(p.petData);
                }

                _db.SaveChanges();

                return RedirectToAction(nameof(LogIn));
            }
            else
            {
                if (p.fPhotodata != null)
                {
                    string photName = Guid.NewGuid().ToString() + Path.GetExtension(p.fPhotodata.FileName);
                    var uploads = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot/upImage");
                    var path = Path.Combine(uploads, photName);
                    p.fPhotodata.CopyTo(new FileStream(path, FileMode.Create));
                    p.merberData.FMePhoto = "/" + photName;
                }

                _db.Update(p.merberData);

                HttpContext.Session.SetObject(CDictionary.SK_LOGINED_CUSTOMER, p.merberData);

                if (selectPet == "1" && p.petData.FPeMemNumber != null)
                {
                    if (p.fPePhotodata != null)
                    {
                        string photName = Guid.NewGuid().ToString() + Path.GetExtension(p.fPePhotodata.FileName);
                        var uploads = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot/upImage");
                        var path = Path.Combine(uploads, photName);
                        p.fPePhotodata.CopyTo(new FileStream(path, FileMode.Create));
                        p.petData.FPePhoto = "/" + photName;
                    }

                    _db.Update(p.petData);
                }
                _db.SaveChanges();
            }

            _db.SaveChanges();

            return RedirectToAction(nameof(MemberProfile));
        }

        public IActionResult RegisteredShortTime()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(Clogin Mail)
        {
            HttpContext.Session.SetObject(CDictionary.SK_MAILCODE, SD.CodeCreate(6));

            string fEmail = (Mail.txtMail);//抓取mail用戶           
            TMemberdata cust = _db.TMemberdata.FirstOrDefault(t => t.FMeMail == fEmail);

            if (fEmail == null)
            {
                HttpContext.Session.SetObject(CDictionary.SK_ERROR, "請勿空白");
                return View();
            }

            if (cust != null)
            {
                HttpContext.Session.SetObject(CDictionary.SK_CHANGEMAIL, cust);
                SD.SendEmail(fEmail, HttpContext.Session.GetObject<string>(CDictionary.SK_MAILCODE));
                return RedirectToAction("ComfirmPass");
            }

            HttpContext.Session.SetObject(CDictionary.SK_ERROR, "無此帳號");
            return View();
        }

        public IActionResult ComfirmPass()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ComfirmPass(Clogin Password)
        {
            if (!HttpContext.Session.GetObject<string>(CDictionary.SK_MAILCODE).Equals(Password.MailCord))
            {
                HttpContext.Session.SetObject(CDictionary.SK_PASS, "驗證碼錯誤");
                return RedirectToAction("ComfirmPass");
            }

            return RedirectToAction("EditPassword");
        }

        public IActionResult EditPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditPassword(Clogin Password)
        {
            var Cust = HttpContext.Session.GetObject<TMemberdata>(CDictionary.SK_CHANGEMAIL);

            TMemberdata changeData = _db.TMemberdata.FirstOrDefault(m => m.FMeMail == Cust.FMeMail);

            if (Password.txtPassword == Password.txtNewPassword)
            {
                changeData.FMePass = Password.txtPassword;
                _db.SaveChanges();
            }
            else
            {
                HttpContext.Session.SetObject(CDictionary.SK_ERROR, "與前一次輸入密碼不同");
                return View();
            }

            return RedirectToAction("Login");
        }
    }
}

