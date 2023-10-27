using ProductDelivery.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductDelivery.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Category()
        {
            var db = new ProductDatabaseEntities();
            var data = db.Categories.ToList();
            return View(data);
        }
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Category cg)
        {
            var db = new ProductDatabaseEntities();
            db.Categories.Add(cg);
            db.SaveChanges();
            return RedirectToAction("Category");

        }
        public ActionResult Delete(int id)
        {
            var db = new ProductDatabaseEntities();
            var data = db.Categories.Find(id);
            db.Categories.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Category");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var db = new ProductDatabaseEntities();
            var data = db.Categories.Find(Id);
            return View(data);

        }
        [HttpPost]
        public ActionResult Edit(Category cg)
        {
            var db = new ProductDatabaseEntities();
            var ex = db.Categories.Find(cg.Id);
            ex.Id = cg.Id;
            db.SaveChanges();
            return RedirectToAction("Category");


        }
        public ActionResult Details(int id)
        {
            var db = new ProductDatabaseEntities();
            var dept = db.Categories.Find(id);

            return View(dept);

        }
    }
}