using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LLWP_Test.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BookingFirstPage()
        {
            return View();
        }

        public ActionResult BookingCalendar()
        {
            return View();
        }

        public ActionResult BookingRoomSelect()
        {
            return View();
        }

        public ActionResult BookingPayment()
        {
            return View();
        }

        public ActionResult PaypalPartial()
        {
            //List<CartVM> cart = Session["cart"] as List<CartVM>;

            return View();
        }
    }
}