using System;
using System.Linq;
using System.Net.Mail;
using LLWP_Core.Utility;
using Microsoft.AspNetCore.Mvc;
using LLWP_Core.Models;
using LLWP_Core.ViewModels;
using System.IO;
using Microsoft.Extensions.Hosting;

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


        public void SendEmail(string emailAddress)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(emailAddress);
            msg.From = new MailAddress("longlifewithpet@gmail.com", "榕沛社區", System.Text.Encoding.UTF8);
            msg.Subject = "加入榕沛會員：繼續完成信箱認證";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = "測試一下"; 
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("longlifewithpet@gmail.com", "llwp3135");
            client.Host = "smtp.gmail.com"; 
            client.Port = 25;
            client.EnableSsl = true; 
            client.Send(msg); 
            client.Dispose();
            msg.Dispose();
        }

        // GET: Members
        public IActionResult MemberProfile()
        {
            if (HttpContext.Session.GetObject<TMemberdata>(CDictionary.SK_LOGINED_CUSTOMER) == null)
                return RedirectToAction("Login");

            return View();
        }

        public IActionResult LogIn()
        {
            var code = HttpContext.Session.GetObject<string>(CDictionary.SK_CODE);
            //string code = Session[Cdictionary.SK_CODE] as string;//驗證碼
            code = SD.CodeCreate(4);
            HttpContext.Session.SetObject(CDictionary.SK_CODE, code);
            ViewBag.CODE = code;

            return View();
        }

        [HttpPost]
        public IActionResult LogIn(Clogin p)
        {
            var code = HttpContext.Session.GetObject<string>(CDictionary.SK_CODE);
            if (!code.Equals(p.txtCord))
            {
                HttpContext.Session.SetObject("SK_AUTHERROR","驗證碼錯誤，登入失敗");
                return RedirectToAction("Login");
            }

            string fEmail = (p.txtAccount);
            TMemberdata cust = _db.TMemberdata.FirstOrDefault(t => t.FMeMail == fEmail && t.FMePass.Equals(p.txtPassword));

            if (cust == null)
            {
                HttpContext.Session.SetObject("SK_AUTHERROR", "帳密錯誤，登入失敗");
                return RedirectToAction("Login");
            }

            HttpContext.Session.SetObject(CDictionary.SK_LOGINED_CUSTOMER, cust);
            return RedirectToAction("MemberProfile");
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

            var memberData = new TMemberdata
            {
                FMeId = memberdbdata.FMeId,
                FMeNumber = memberdbdata.FMeNumber,
                FMeGender = memberdbdata.FMeGender,
                FMeName = memberdbdata.FMeName,
                FMeBirth = memberdbdata.FMeBirth,
                FMeMail = memberdbdata.FMeMail,
                FMePass = memberdbdata.FMePass,
                FMePhone = memberdbdata.FMePhone,
                FMeAge = memberdbdata.FMeAge,
                FMePersonId = memberdbdata.FMePersonId,
                FMePhoto = memberdbdata.FMePhoto,
                FMeEmerName = memberdbdata.FMeEmerName,
                FMeEmerPhone = memberdbdata.FMeEmerPhone
            };

            if (petdbData != null)
            {
                var petData = new TMempetdata
                {
                    FPeMemNumber = petdbData.FPeMemNumber,
                    FPeVarity = petdbData.FPeVarity,
                    FPeAge = petdbData.FPeAge,
                    FPeWeight = petdbData.FPeWeight,
                    FPeBirth = petdbData.FPeBirth,
                    FPeSex = petdbData.FPeSex,
                    FPeVac = petdbData.FPeVac,
                    FPeFix = petdbData.FPeFix,
                    FPePhoto = petdbData.FPePhoto
                };

                p = new ViewModelMP
                {
                    merberData = memberData,
                    petData = petData,
                };
            }
            else
            {
                var TMempetdataidEqualZero = new TMempetdata { FPeId = 0 };
                p = new ViewModelMP { merberData = memberData, petData = TMempetdataidEqualZero };
            }
            

            return View(p);
        }

        [HttpPost]
        public IActionResult RegisteredLongTime(ViewModelMP p)
        {
            string selectPet = Request.Form["selectPet"];
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

                var code = "";
                code = SD.CodeCreate(6);

                p.merberData.FMeNumber = code;
                p.petData.FPeMemNumber = code;

                if (ModelState.IsValid)
                    _db.TMemberdata.Add(p.merberData);
                else
                    return View();

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
            }
            
            _db.SaveChanges();

            return RedirectToAction(nameof(MemberProfile));
        }

        public IActionResult RegisteredShortTime()
        {
            return View();
        }

        public string CreateCodeIn()
        {
            var code = SD.CodeCreate(4);
            HttpContext.Session.SetObject(CDictionary.SK_CODE, code);
            return code;
        }

    }
}

