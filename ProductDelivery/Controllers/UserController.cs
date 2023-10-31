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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                var db = new ProductDatabaseEntities();
                var existingUser = db.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

                if (existingUser != null && user.Password == existingUser.Password && existingUser.Type == "Customer")
                {
                    // Authentication successful
                    Session["UserId"] = existingUser.Id; // Store user ID in session

                    // Set a cookie to remember the user's login
                    HttpCookie userCookie = new HttpCookie("UserAuthentication");
                    userCookie["UserId"] = existingUser.Id.ToString();
                    userCookie.Expires = DateTime.Now.AddHours(1); // Set the cookie to expire after a certain time
                    Response.Cookies.Add(userCookie);

                    /*  return RedirectToAction("Order");*/
                    return RedirectToAction("firstPage", "User");
                }


              else  if (existingUser != null && user.Password == existingUser.Password && existingUser.Type == "Admin")
                {
                    // Authentication successful
                    Session["UserId"] = existingUser.Id; // Store user ID in session

                    // Set a cookie to remember the user's login
                    HttpCookie userCookie = new HttpCookie("AdminAuthentication");
                    userCookie["UserId"] = existingUser.Id.ToString();
                    userCookie.Expires = DateTime.Now.AddHours(1); // Set the cookie to expire after a certain time
                    Response.Cookies.Add(userCookie);

                    /*  return RedirectToAction("Order");*/
                    return RedirectToAction("Admin", "Admin");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log it or show an error message to the user
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View(user);
            }
        }

   
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SignUp(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<UserDTO, User>();
                });

                var mapper = new Mapper(config);
                var data = mapper.Map<User>(user);

                try
                {
                    var db = new ProductDatabaseEntities();
                    db.Users.Add(data);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    // Handle the exception, e.g., log it or show an error message to the user
                    ViewBag.ErrorMessage = "An error occurred during sign-up: " + ex.Message;
                    return View(user);
                }
            }

            return View(user);
        }

        public ActionResult LogoutAdmin()
        {

            if (Request.Cookies["AdminAuthentication"] != null)
            {
                var adminCookie = new HttpCookie("AdminAuthentication");
                adminCookie.Expires = DateTime.Now.AddYears(-1); // logout
                Response.Cookies.Add(adminCookie);
            }


            return RedirectToAction("Login");
        }

 


        [HttpPost]
        public ActionResult LogoutUser()
        {
            // Clear the session
            Session.Clear();

            // Clear the user authentication cookie
            if (Request.Cookies["UserAuthentication"] != null)
            {
                Response.Cookies["UserAuthentication"].Expires = DateTime.Now.AddDays(-1);
            }

            return RedirectToAction("User");
        }


        /*   public ActionResult firstPage()
           {

               int userId = 0;
               var db = new ProductDatabaseEntities();

               if (Request.Cookies["UserAuthentication"] != null && int.TryParse(Request.Cookies["UserAuthentication"]["UserId"], out userId))
               {
                   var userOrders = db.Orders.Where(o => o.CustomerId == userId).ToList();
                   return View(userOrders);
               }


               return View("Error");


           }*/

        public ActionResult firstPage()
        {
            int userId = 0;
            var db = new ProductDatabaseEntities();

            if (Request.Cookies["UserAuthentication"] != null && int.TryParse(Request.Cookies["UserAuthentication"]["UserId"], out userId))
            {
                // Retrieve a list of orders for the user with the given user ID
                var userOrders = db.Orders.Where(o => o.CustomerId == userId).ToList();
                return View(userOrders); // Pass the list of orders to the view
            }

            return View("Error");
        }




        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var db = new ProductDatabaseEntities();
            var data = db.Users.Find(Id);
            return View(data);

        }
        [HttpPost]
        public ActionResult Edit(User u)
        {
            var db = new ProductDatabaseEntities();
            var ex = db.Users.Find(u.Id);

            if (ex != null)
            {
                
                ex.Id = u.Id;
                ex.Username = u.Username;
                ex.Password = u.Password;
                ex.Type = u.Type;
              

                db.SaveChanges();
            }

            return RedirectToAction("User");
        }
        public ActionResult Details(int id)
        {
            var db = new ProductDatabaseEntities();
            var user1 = db.Users.Find(id);

            return View(User);

        }


        [HttpGet]
        public ActionResult Deshboard()
        {
            if (Request.Cookies["UserAuthentication"] != null)
            {
                string UserUserName = Request.Cookies["UserAuthentication"]["UserUserName"];

                var db = new ProductDatabaseEntities();
                var data = db.Products.ToList();
                return View(data);

            }


            return RedirectToAction("Index");
        }


        public ActionResult AddToCart(int id, string name, float Price, float Quantity, int CategoryId)
        {
            if (Session["ProductBasket"] == null)
            {
                Session["ProductBasket"] = new List<CartItem>();
            }

            List<CartItem> productBasket = (List<CartItem>)Session["ProductBasket"];




            var existingProduct = productBasket.FirstOrDefault(item => item.id == id);
            // Checking is that same product already in the cart
            if (existingProduct != null)
            {
                // only increses the quantity
                existingProduct.Quantity += Quantity;
             
            }
            else
            {
                // add product in cart
                productBasket.Add(new CartItem
                {
                    id = id,
                    name = name,
                    Price = Price,
                    Quantity = Quantity,
                    CategoryId = CategoryId,
                   
                });
            }

            Session["ProductBasket"] = productBasket;

            return RedirectToAction("Deshboard");

        }

        [HttpGet]
        public ActionResult ViewCart()
        {
            // Retrieve the shopping cart items from the session
            List<CartItem> cartItems = Session["ProductBasket"] as List<CartItem>;

            if (cartItems == null || cartItems.Count == 0)
            {
                ViewBag.Message = "Your cart is empty.";
            }
            else
            {
               
                ViewBag.cartItems = cartItems;
            }

            return View("ViewCart");
        }


        /*    [HttpGet]
            public ActionResult ViewCart()
            {

                // Retrieve the shopping cart items from the session
                List<CartItem> cartItems = Session["ProductBasket"] as List<CartItem>;

                if (cartItems == null || cartItems.Count == 0)
                {
                    ViewBag.Message = "Your cart is empty.";
                }
                else
                {
                    // store in details in viewbag
                    ViewBag.ProductIdsInCart = cartItems;
                }

                return View("ViewCart");
            }
    */


        [HttpPost]
        public ActionResult ConfirmOrder()
        {
            // Retrieve the product details from the session
            var productIdsInCart = Session["ProductBasket"] as List<CartItem>;

            if (productIdsInCart != null)
            {
                // Get uer id from cookie
                int UserId;
                if (Request.Cookies["UserAuthentication"] != null && int.TryParse(Request.Cookies["UserAuthentication"]["UserId"], out UserId))
                {
                    using (var db = new ProductDatabaseEntities())
                    {
                        using (var transaction = db.Database.BeginTransaction())
                        {
                           
                            try
                            {
                                // Create a new order for each checkout
                                var orderD = new Order
                                {
                                    Status = "Ordered",
                                    CustomerId = UserId,
                                    Date = DateTime.Now
                                };
                                db.Orders.Add(orderD);
                                db.SaveChanges();

                                // Add products to the order
                                foreach (var product in productIdsInCart)
                                {
                                    var orderM = new OrderDetail
                                    {                  
                                       Id= orderD.Id,
                                        ProductId = product.id,
                                        Quantity = product.Quantity,
                                        Price = product.Price * product.Quantity
                                    };
                                    db.OrderDetails.Add(orderM);
                                }

                                db.SaveChanges();

                                // after update both table delete the seassion
                                Session.Remove("ProductBasket");

                                transaction.Commit(); // all the operations within the transaction have been completed successfully.


                                return RedirectToAction("ThankYouForOrder");
                            }
                            catch (Exception ex)
                            {
                                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                                transaction.Rollback();
                            }
                        }
                    }
                }
            }

            return RedirectToAction("Login");
        }





        public ActionResult ThankYouForOrder()
        {
            return View();
        }

        public ActionResult OrderHistory(int userId)
        {
            var db = new ProductDatabaseEntities();

            var userOrders = db.Orders.Where(o => o.CustomerId == userId).ToList();

            return View(userOrders);
        }


    }

}



