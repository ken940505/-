using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LLWP_Core.Models;
using LLWP_Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace LLWP_Core.Controllers
{
    public class TryPetController : Controller
    {
        private readonly dbLLWPContext _db;
        private readonly IHostEnvironment hostingEnvironment;

        public TryPetController(dbLLWPContext db, IHostEnvironment environment)
        {
            _db = db;
            hostingEnvironment = environment;
        }
        public IActionResult List(string txtKeyword)
        {
            var tryPets = new TTryPetTableVM
            {
                tryPetTableList = _db.TTryPetTable.OrderBy(o=>o.FTryPetId).ToList()
            };

            if (!string.IsNullOrEmpty(txtKeyword))
            {
                tryPets = new TTryPetTableVM
                {
                    tryPetTableList = _db.TTryPetTable.Where(o=>o.FTryPetName.Contains(txtKeyword) || o.FTryPetNum.Equals(txtKeyword)).ToList()
                };
            }
            //string keyword = Request.Form["txtKeyword"];
            //if (string.IsNullOrEmpty(keyword))
            //{
            //tryPets = from p in _db.TTryPetTable
            //          select p;
            //}
            //else
            //{
            //    tryPets = from p in _db.TTryPetTable
            //              where p.FTryPetName.Contains(keyword) || p.FTryPetNum.Equals(keyword)
            //              select p;
            //}
            return View(tryPets);

            //var tryPets = from p in (new dbLLWPEntities1()).tTryPetTable
            //              orderby p.fTryPetNum ascending
            //            select p;
            //return View(tryPets);
        }

        public ActionResult Show()
        {
            return View();
        }

        public ActionResult ShowList()
        {
            return RedirectToAction("List");
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult Room()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TTryPetTableVM p)
        {
            //fImage是一開始接要上傳的檔案，fTryPetPhoto是資料庫的資料放圖片的欄位
            if (p.fImage != null)//若有要上傳的檔案(此處不能用p.fTryPetPhoto，因為創新的前本來就是null)
            {
                string photName = Guid.NewGuid().ToString() + Path.GetExtension(p.fImage.FileName);
                var uploads = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot/upImage");
                var path = Path.Combine(uploads, photName);
                p.fImage.CopyTo(new FileStream(path, FileMode.Create));
                p.tryPetTable.FTryPetPhoto = "/" + photName;
            }
            if (p.tryPetTable.FTryPetPhoto != null)
            {
                _db.TTryPetTable.Add(p.tryPetTable);
                _db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        //GET:data
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            TTryPetTable p = _db.TTryPetTable.FirstOrDefault(m => m.FTryPetId == id);
            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(TTryPetTableVM p)
        {
            if (string.IsNullOrEmpty(p.tryPetTable.FTryPetName))
                return RedirectToAction("List");

            TTryPetTable pNew = _db.TTryPetTable.FirstOrDefault(m => m.FTryPetId == p.tryPetTable.FTryPetId);

            if (pNew != null)
            {
                //照片檔案上傳，有新檔案要上傳(p.fImage!=null)才執行，否則會有例外錯誤
                if (p.tryPetTable.FTryPetPhoto != null && p.fImage != null)
                {
                    string photName = Guid.NewGuid().ToString() + Path.GetExtension(p.fImage.FileName);
                    var uploads = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot/upImage");
                    var path = Path.Combine(uploads, photName);
                    p.fImage.CopyTo(new FileStream(path, FileMode.Create));
                    p.tryPetTable.FTryPetPhoto = "/" + photName;
                    _db.TTryPetTable.Add(p.tryPetTable);
                    //db.SaveChanges();
                }
                pNew.FTryPetNum = p.tryPetTable.FTryPetNum;
                pNew.FTryPetName = p.tryPetTable.FTryPetName;
                pNew.FTryPetVar = p.tryPetTable.FTryPetVar;
                pNew.FTryPetAge = p.tryPetTable.FTryPetAge;
                pNew.FTryPetWei = p.tryPetTable.FTryPetWei;
                pNew.FTryPetSex = p.tryPetTable.FTryPetSex;
                pNew.FTryPetNature = p.tryPetTable.FTryPetNature;
                pNew.FTryPetVac = p.tryPetTable.FTryPetVac;
                pNew.FTryPetFix = p.tryPetTable.FTryPetFix;
                pNew.FTryPetPhoto = p.tryPetTable.FTryPetPhoto;
                _db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            TTryPetTable p = _db.TTryPetTable.FirstOrDefault(m => m.FTryPetId == id);
            if (p != null)
            {
                _db.TTryPetTable.Remove(p);
                _db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
