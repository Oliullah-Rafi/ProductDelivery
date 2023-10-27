using ProductDelivery.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductDelivery.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Product()
        {
            var db = new ProductDatabaseEntities();
            var data = db.Products.ToList();
            return View(data);
         
        }
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Product p)
        {
            var db = new ProductDatabaseEntities();
            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction("Product");

        }
        public ActionResult Delete(int id)
        {
            var db = new ProductDatabaseEntities();
            var data = db.Products.Find(id);
            db.Products.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Product");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var db = new ProductDatabaseEntities();
            var data = db.Products.Find(Id);
            return View(data);

        }
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            var db = new ProductDatabaseEntities();
            var ex = db.Products.Find(p.Id);
            ex.Id = p.Id;
            db.SaveChanges();
            return RedirectToAction("Product");


        }
        public ActionResult Details(int id)
        {
            var db = new ProductDatabaseEntities();
            var dept = db.Products.Find(id);

            return View(dept);

        }


    }
}