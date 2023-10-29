using AutoMapper;
using ProductDelivery.DTOS;
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
        public ActionResult Create(CategoryDTO cg)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<CategoryDTO, Category>();
                });

                var mapper = new Mapper(config);
                var data = mapper.Map<Category>(cg);

                var db = new ProductDatabaseEntities();
                db.Categories.Add(data); // Assuming you want to add a Category, not an Order
                db.SaveChanges();
                return RedirectToAction("Category"); // Redirect to the Category action
            }

            return View(cg);
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
        public ActionResult Edit(CategoryDTO cg)
        {
            if (ModelState.IsValid)
            {
                var db = new ProductDatabaseEntities();
                var ex = db.Categories.Find(cg.Id);

                if (ex != null)
                {
                    // Configure AutoMapper to map from CategoryDTO to Category
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<CategoryDTO, Category>();
                    });

                    var mapper = new Mapper(config);

                    // Map the updated data from CategoryDTO (cg) to the existing Category entity (ex)
                    ex = mapper.Map(cg, ex);

                    db.SaveChanges();
                }

                return RedirectToAction("Category");
            }

            return View(cg);
        }

        public ActionResult Details(int id)
        {
            var db = new ProductDatabaseEntities();
            var category = db.Categories.Find(id);

            if (category != null)
            {
                // Configure AutoMapper to map from Category to CategoryDTO
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Category, CategoryDTO>();
                });

                var mapper = new Mapper(config);

                // Map the Category entity to a CategoryDTO
                var categoryDTO = mapper.Map<CategoryDTO>(category);

                return View(categoryDTO);
            }

            // Handle the case when the category is not found, e.g., redirect or display an error message
            // For example, you can redirect to the Category view:
            return RedirectToAction("Category");
        }

    }
}