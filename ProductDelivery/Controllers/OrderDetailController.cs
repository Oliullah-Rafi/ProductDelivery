using ProductDelivery.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductDelivery.Views.Category
{
    public class OrderDetailController : Controller
    {
        // GET: OrderDetail
        public ActionResult OrderDetail()
        {
            var db = new ProductDatabaseEntities();
            var data = db.OrderDetails.ToList();
            return View(data);

        }
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(OrderDetail od)
        {
            var db = new ProductDatabaseEntities();
            db.OrderDetails.Add(od);
            db.SaveChanges();
            return RedirectToAction("OrderDetail");

        }
        public ActionResult Delete(int id)
        {
            var db = new ProductDatabaseEntities();
            var data = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(data);
            db.SaveChanges();
            return RedirectToAction("OrderDetail");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var db = new ProductDatabaseEntities();
            var data = db.OrderDetails.Find(Id);
            return View(data);

        }
        [HttpPost]
        public ActionResult Edit(OrderDetail ordd)
        {
            var db = new ProductDatabaseEntities();
            var ex = db.OrderDetails.Find(ordd.Id);

            if (ex != null)
            {
                // Update the properties of the existing product entity with the values from the edited product
                ex.Id = ordd.Id;
                ex.OrderID = ordd.OrderID;
                ex.ProductId = ordd.ProductId;
                ex.Price = ordd.Price;
                ex.Quantity = ordd.Quantity;

                db.SaveChanges();
            }

            return RedirectToAction("OrderDetail");
        }
        /*      [HttpPost]
              public ActionResult Edit(OrderDetail od)
              {
                  var db = new ProductDatabaseEntities();
                  var ex = db.OrderDetails.Find(od.Id);
                  ex.Id = od.Id;
                  db.SaveChanges();
                  return RedirectToAction("OrderDetail");


              }*/
        public ActionResult Details(int id)
        {
            var db = new ProductDatabaseEntities();
            var dept = db.OrderDetails.Find(id);

            return View(dept);

        }
    }
}