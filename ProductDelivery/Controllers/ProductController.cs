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
        public ActionResult Create(ProductDTO p)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ProductDTO, Product>();
                });

                var mapper = new Mapper(config);
                var data = mapper.Map<Product>(p);

                var db = new ProductDatabaseEntities();
                db.Products.Add(data); // Assuming you want to add a Product, not a Category
                db.SaveChanges();
                return RedirectToAction("Product"); // Redirect to the Product action
            }

            return View(p);
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
        public ActionResult Edit(ProductDTO p)
        {
            if (ModelState.IsValid)
            {
                var db = new ProductDatabaseEntities();
                var ex = db.Products.Find(p.Id);

                if (ex != null)
                {
                    // Configure AutoMapper to map from ProductDTO to Product
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<ProductDTO, Product>();
                    });

                    var mapper = new Mapper(config);

                    // Map the updated data from ProductDTO (p) to the existing Product entity (ex)
                    ex = mapper.Map(p, ex);

                    db.SaveChanges();
                }

                return RedirectToAction("Product");
            }

            return View(p);
        }

        public ActionResult Details(int id)
        {
            var db = new ProductDatabaseEntities();
            var product = db.Products.Find(id);

            if (product != null)
            {
                // Configure AutoMapper to map from Product to ProductDTO
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Product, ProductDTO>();
                });

                var mapper = new Mapper(config);

                // Map the Product entity to a ProductDTO
                var productDTO = mapper.Map<ProductDTO>(product);

                return View(productDTO);
            }

            // Handle the case when the product is not found, e.g., redirect or display an error message
            // For example, you can redirect to the Product view:
            return RedirectToAction("Product");
        }



    }
}