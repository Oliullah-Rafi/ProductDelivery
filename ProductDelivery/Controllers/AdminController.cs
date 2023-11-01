using ProductDelivery.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductDelivery.Controllers
{
    public class AdminController : Controller
    {
     

        // GET: Admin
        public ActionResult Admin()
        {

            if (Request.Cookies["AdminAuthentication"] != null)
            {
                var db = new ProductDatabaseEntities();
                var data = db.Orders.ToList();
                return View(data);

            }
            return RedirectToAction("Admin", "User");

        }


        public ActionResult ProcessItem(int orderId)
        {
            using (var db = new ProductDatabaseEntities())
            {
                // Find all Order using order id
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);

                if (order != null)
                {
                    order.Status = "Processing";

                    // Find all products from OrderDetails accorading to order id
                    var orderProducts = db.OrderDetails.Where(o => o.Id == orderId).ToList();

                    foreach (var orderDetail in orderProducts)
                    {
                        var product = db.Products.FirstOrDefault(p => p.Id == orderDetail.ProductId);

                        if (product != null)
                        {

                           // product.ProductCount -= OrderDetails.Quantity;
                        }
                    }

                    db.SaveChanges();

                    return RedirectToAction("Admin");

                }
            }

            return View("Error");
        }


        public ActionResult DeclineItem(int orderId)
        {
            using (var db = new ProductDatabaseEntities())
            {

                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);

                if (order != null)
                {

                    order.Status = "Decline";
                    db.SaveChanges();


                    return RedirectToAction("Admin");
                }
                else
                {
                    return RedirectToAction("ErrorPage");
                }
            }
        }


        public ActionResult GetOrderItem(int orderId)
        {
            using (var db = new ProductDatabaseEntities())
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);

                if (order != null)
                {
                    var Customer = db.Users.FirstOrDefault(c => c.Id == order.CustomerId);
                    var OrderDetail = db.OrderDetails.Where(om => om.Id == orderId).ToList();

                    var products = OrderDetail
                        .Select(om => new
                        {
                            ProductName = db.Products.FirstOrDefault(p => p.Id == om.ProductId).Name,
                            Quantity = om.Quantity
                        })
                        .ToList();

                    var orders = new
                    {
                        CustomerName = Customer.Username,
                     
                        OrderedProducts = products
                    };

                    return Json(orders, JsonRequestBehavior.AllowGet);
                }

                return Json("Order not found", JsonRequestBehavior.DenyGet);
            }
        }






    }
}