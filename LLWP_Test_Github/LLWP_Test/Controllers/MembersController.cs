using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LLWP_Test.Controllers
{
    public class MembersController : Controller
    {
        // GET: Members
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult RegisteredPet()
        {
            return View();
        }

        public ActionResult RegisteredLongTime()
        {
            return View();
        }

        public ActionResult RegisteredShortTime()
        {
            return View();
        }

        public ActionResult MemberProfile()
        {
            return View();
        }
    }
}