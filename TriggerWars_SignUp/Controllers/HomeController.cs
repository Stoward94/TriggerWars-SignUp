using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using TriggerWars_SignUp.Models;

namespace TriggerWars_SignUp.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext db { get; set; }

        public HomeController()
        {
            db = new ApplicationDbContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SignUp model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            SignUp signUp = new SignUp
            {
                Email = model.Email,
                SignUpDate = DateTime.Now
            };

            //Check for duplicate email
            var query = from s in db.SignUps
                            select s;

            var duplicate = query.Any(x => x.Email == model.Email);

            if(duplicate)
            {
                ModelState.AddModelError("", "This email has already been registered! Have another?");
                return View();
            }

            db.SignUps.Add(signUp);
            db.SaveChanges();

            model.Email = "";

            TempData["Success"] = "Thanks for registering your interest in the TriggerWars beta program! " +
                "\nConnect with the TriggerWars Facebook and Twitter accounts to see further development and beta news.";

            return View();                        
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}