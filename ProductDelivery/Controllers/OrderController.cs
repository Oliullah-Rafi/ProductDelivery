using ProductDelivery.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductDelivery.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Order()
        {
            var db = new ProductDatabaseEntities();
            var data = db.Orders.ToList();
            return View(data);
        }
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Order o)
        {
            var db = new ProductDatabaseEntities();
            db.Orders.Add(o);
            db.SaveChanges();
            return RedirectToAction("Order");

        }
        public ActionResult Delete(int id)
        {
            var db = new ProductDatabaseEntities();
            var data = db.Orders.Find(id);
            db.Orders.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Order");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var db = new ProductDatabaseEntities();
            var data = db.Orders.Find(Id);
            return View(data);

        }
        [HttpPost]
        public ActionResult Edit(Order o)
        {
            var db = new ProductDatabaseEntities();
            var ex = db.Orders.Find(o.Id);
            ex.Id = o.Id;
            db.SaveChanges();
            return RedirectToAction("Order");


        }
        public ActionResult Details(int id)
        {
            var db = new ProductDatabaseEntities();
            var dept = db.Orders.Find(id);

            return View(dept);

        }
    }
}