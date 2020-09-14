using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LLWP_Core.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace LLWP_Core.Controllers
{
    public class RoomOrderPagedListController : Controller
    {
        private readonly dbLLWPContext _db;
        public RoomOrderPagedListController(dbLLWPContext db)
        {
            _db = db;
        }
        int pageSize = 10;
        public ActionResult Index(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var roomOrders = _db.TOrTable.OrderBy(m => m.FOrNum).ToList();
            var result = roomOrders.ToPagedList(currentPage, pageSize);
            return View(result);
        }
    }
}
