using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Controllers
{
    public class AccountController : Controller
    {
        // GET: Default
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user) 
        {
            if (ModelState.IsValid)
            { 
                MVCTrainingEntities dbContext = new MVCTrainingEntities();
                var userData = dbContext.Users.Where(x => x.username.ToLower() == user.username.ToLower() && x.password == user.password).FirstOrDefault();
                if(userData!=null)
                {
                    Session["UserId"] = userData.userId;
                    Session["Username"] = userData.username;
                    Session["Role"] = userData.role;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}