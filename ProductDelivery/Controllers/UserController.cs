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

                if (existingUser != null)
                {
                    // Authentication successful
                    Session["UserId"] = existingUser.Id; // Store user ID in session

                    // Set a cookie to remember the user's login
                    HttpCookie userCookie = new HttpCookie("UserAuthentication");
                    userCookie["UserId"] = existingUser.Id.ToString();
                    userCookie.Expires = DateTime.Now.AddHours(1); // Set the cookie to expire after a certain time
                    Response.Cookies.Add(userCookie);

                    /*  return RedirectToAction("Order");*/
                    return RedirectToAction("Order", "Order");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log it or show an error message to the user
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View(user);
            }
        }

        //last_ChangedCode
        /*    // GET: User
            public ActionResult Login()
            {
                return View();
            }

            *//*       [HttpPost]
                   public ActionResult Login(User User)
                   {
                       return View();
                       var db = new ProductDatabaseEntities();
                       var existingUser = db.Users.FirstOrDefault(u => u.Username == User.Username);
                       return View();
                   }*//*

            [HttpPost]

            public ActionResult Login(User user)
            {
                var db = new ProductDatabaseEntities();
                var existingUser = db.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

                if (existingUser != null)
                {
                    // Authentication successful
                    // You can set the user's session or cookie here for authentication
                    //return RedirectToAction("Index", "Home"); // Redirect to the desired page after successful login
                    return RedirectToAction("Welcome");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                    return View(user); // Return to the login form with an error message


                }
            }*/

        /*        public ActionResult Login(User user)
                {
                    var db = new ProductDatabaseEntities();
                    var existingUser = db.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

                    if (existingUser != null)
                    {
                        // Authentication succeeded, you can implement your logic here.
                        // For example, you can set a session variable to indicate the user is logged in.
                        Session["UserId"] = existingUser.Id;

                        return RedirectToAction("Login", "User"); // Redirect to the home page or wherever you want.
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                        return View();
                    }
                }*/
        /*
                [HttpGet]
                public ActionResult SignUp()
                {
                    return View();
                }
                [HttpPost]
                public ActionResult SignUp(User u)
                {
                    var db = new ProductDatabaseEntities();
                    db.Users.Add(u);
                    db.SaveChanges();
                    return RedirectToAction("Login");

                }*/

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

        /*         public ActionResult Welcome()
             { 

                 int? userId = Session["UserId"] as int?;
                 if (userId != null)
                 {
                     // User is logged in; you can use the userId to fetch user-specific data
                     return View();
                 }
                 else
                 {
                     // Handle the case where the user is not authenticated
                     return RedirectToAction("Login");
                 }
             }*/


        [HttpPost]
        public ActionResult Logout()
        {
            // Clear the session
            Session.Clear();

            // Clear the user authentication cookie
            if (Request.Cookies["UserAuthentication"] != null)
            {
                Response.Cookies["UserAuthentication"].Expires = DateTime.Now.AddDays(-1);
            }

            return RedirectToAction("Login");
        }
        /*
                public ActionResult Details(int id)
                {
                    var db = new ProductDatabaseEntities();
                    var user = db.Users.Find(id);

                    if (user != null)
                    {
                        // Configure AutoMapper to map from User to UserDTO
                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<User, UserDTO>();
                        });

                        var mapper = new Mapper(config);

                        // Map the User entity to a UserDTO
                        var userDTO = mapper.Map<UserDTO>(user);

                        return View(userDTO);
                    }


                    return View("User");
                }*/
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
                // Update the properties of the existing product entity with the values from the edited product
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



    }

}



