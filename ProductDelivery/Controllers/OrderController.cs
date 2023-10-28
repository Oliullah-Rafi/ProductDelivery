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
        public ActionResult Create(OrderDTO o)
        {
            if (ModelState.IsValid) {
                #region
                var config = new MapperConfiguration(cfg => {

                    cfg.CreateMap<OrderDTO, Order>();
                
                });

                var Mapper = new Mapper(config);
                var data = Mapper.Map<Order>(o);
                #endregion

                var db = new ProductDatabaseEntities();
            db.Orders.Add(data);
            db.SaveChanges();
            return RedirectToAction("Order");

            }

            return View(o);
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
        public ActionResult Edit(OrderDTO o)
        {
            if (ModelState.IsValid)
            {
                var db = new ProductDatabaseEntities();
                var ex = db.Orders.Find(o.Id);

                if (ex != null)
                {
                    // Configure AutoMapper to map from OrderDTO to Order
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<OrderDTO, Order>();
                    });

                    var mapper = new Mapper(config);

                    // Map the updated data from OrderDTO (o) to the existing Order entity (ex)
                    ex = mapper.Map(o, ex);

                    db.SaveChanges();
                }

                return RedirectToAction("Order");
            }

            return View(o);
        }

 

        public ActionResult Details(int id)
        {
            var db = new ProductDatabaseEntities();
            var order = db.Orders.Find(id);

            if (order != null)
            {
                // Configure AutoMapper to map from Order to OrderDTO
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Order, OrderDTO>();
                });

                var mapper = new Mapper(config);

                // Map the Order entity to an OrderDTO
                var orderDTO = mapper.Map<OrderDTO>(order);

                return View(orderDTO);
            }

            return RedirectToAction("Order"); // Redirect to the Order view if the order is not found
        }

    }
}