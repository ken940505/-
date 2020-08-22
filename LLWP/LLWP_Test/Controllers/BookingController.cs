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

        // POST: /Cart/PlaceOrder
        //[HttpPost]
        //public void PlaceOrder()
        //{
        //    // Get cart list
        //    List<CartVM> cart = Session["cart"] as List<CartVM>;

        //    // Get username
        //    string username = User.Identity.Name;

        //    int orderId = 0;

        //    using (Db db = new Db())
        //    {
        //        // Init OrderDTO
        //        OrderDTO orderDTO = new OrderDTO();

        //        // Get user id
        //        var q = db.Users.FirstOrDefault(x => x.Username == username);
        //        int userId = q.Id;

        //        // Add to OrderDTO and save
        //        orderDTO.UserId = userId;
        //        orderDTO.CreatedAt = DateTime.Now;

        //        db.Orders.Add(orderDTO);

        //        db.SaveChanges();

        //        // Get inserted id
        //        orderId = orderDTO.OrderId;

        //        // Init OrderDetailsDTO
        //        OrderDetailsDTO orderDetailsDTO = new OrderDetailsDTO();

        //        // Add to OrderDetailsDTO
        //        foreach (var item in cart)
        //        {
        //            orderDetailsDTO.OrderId = orderId;
        //            orderDetailsDTO.UserId = userId;
        //            orderDetailsDTO.ProductId = item.ProductId;
        //            orderDetailsDTO.Quantity = item.Quantity;

        //            db.OrderDetails.Add(orderDetailsDTO);

        //            db.SaveChanges();
        //        }
        //    }

        //    // Email admin
        //    //var client = new SmtpClient("mailtrap.io", 2525)
        //    //{
        //    //    Credentials = new NetworkCredential("21f57cbb94cf88", "e9d7055c69f02d"),
        //    //    EnableSsl = true
        //    //};
        //    //client.Send("admin@example.com", "admin@example.com", "New Order", "You have a new order. Order number " + orderId);

        //    // Reset session
        //    Session["cart"] = null;
        }
    }
}