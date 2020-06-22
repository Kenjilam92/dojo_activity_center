using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dojo_activity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dojo_activity.Controllers
{
    public class HomeController : Controller
    {
        private Context databases {get;set;}
        private PasswordHasher<User> regHasher = new PasswordHasher<User>() ;
        private PasswordHasher<LoginUser> logHasher = new PasswordHasher<LoginUser>(); 
        public HomeController(Context context)
        {
            databases = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Domain()
        {
            if(HttpContext.Session.GetInt32("userId") == null)
            {
                return Redirect("/signin");
            }
            return Redirect("/home");
        }
        ///////////////////////////////// Login Register Logout
        [Route("signin")]
        public IActionResult Index()
        {

            return View();
        }
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(User newuser)
        {
            if(ModelState.IsValid)
            {   
                if(databases.Users.Any(u => u.Email == newuser.Email))
                {
                    ModelState.AddModelError("Email","This email is already registed. Please Login!");
                    return View("Index");
                }
                string hash = regHasher.HashPassword(newuser,newuser.Password);
                newuser.Password = hash;
                databases.Users.Add(newuser);
                databases.SaveChanges();
                HttpContext.Session.SetInt32("userId", newuser.UserId);
                return Redirect("/home");
            }
            return View("Index");
        }
        [Route("login")]
        public IActionResult LogIn(LoginUser user)
        {
            if(ModelState.IsValid)
            {
                User userInDB = databases.Users.FirstOrDefault(u => u.Email == user.LoginEmail);
                if( userInDB==null)
                {
                    ModelState.AddModelError("LoginEmail","Invalid Email or Password");
                    return View("Index");
                }
                var decode = logHasher.VerifyHashedPassword(user, userInDB.Password, user.LoginPassword);
                if( decode==0)
                {
                    ModelState.AddModelError("LoginPassword","Invalid Email or Password");
                    return View("Index");
                }
                HttpContext.Session.SetInt32 ("userId", userInDB.UserId);
                return Redirect("/home");
            }
            return View("Index");

            ////////////////////// Home
        }

        [HttpGet]
        [Route("home")]
        public IActionResult Home ()
        {
            if(HttpContext.Session.GetInt32("userId") == null)
            {   
                return Redirect("/signin");
            }
            ViewBag.User = databases.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
            ViewBag.AllActs= databases.Acts
                    .OrderBy(a => a.Date)
                    .OrderBy(a => a.Time)
                    .Include(a => a.Creater)
                    .Include(a => a.AllParties)
                    .Where(a => a.Date >= DateTime.Now)
                    .ToList();
            return View();
        }
        [Route("new")]
        public IActionResult NewAct()
        {
            if(HttpContext.Session.GetInt32("userId") == null)
            {
                return Redirect("/signin");
            }
            return View();
        } 

        [HttpPost]
        [Route("create")]
        public IActionResult Create(Act newAct)
        {   
            if(HttpContext.Session.GetInt32("userId") == null)
            {
                return Redirect("/signin");
            }
            if(ModelState.IsValid)
            {
                newAct.UserId =  (int)HttpContext.Session.GetInt32("userId");
                databases.Acts.Add(newAct);
                databases.SaveChanges();
                return Redirect("/home");
            }
            return View("NewAct");
        }

        //////////////////// Activity Details

        [HttpGet]
        [Route("activity/{id}")]
        public IActionResult ActDetails(int id)
        {   
            if(HttpContext.Session.GetInt32("userId") == null)
            {
                return Redirect("/signin");
            }
            ViewBag.Act= databases.Acts
                .Include(a => a.AllParties)
                .ThenInclude(associate => associate.User)
                .Include(a => a.Creater)
                .FirstOrDefault(a => a.ActId == id);
            ViewBag.User = databases.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
            return View();
        }
        [Route("activity/{id}/{cmd}")]
        public IActionResult ActChange(int id, string cmd)
        {
            if(HttpContext.Session.GetInt32("userId") == null)
            {
                return Redirect("/signin");
            }
            Act Activity = databases.Acts.FirstOrDefault(a =>a.ActId == id );
            User User = databases.Users.FirstOrDefault( u => u.UserId == HttpContext.Session.GetInt32("userId"));
            if ( cmd == "delete" && Activity.UserId == User.UserId)
            {
                databases.Acts.Remove(Activity);
                databases.SaveChanges();
                return Redirect("/home");
            }
            else if (cmd == "join")
            {
                Associate join = new Associate ();
                join.UserId = User.UserId;
                join.ActId = Activity.ActId;
                databases.Associates.Add(join);
                databases.SaveChanges();
                return Redirect($"/activity/{id}");
            }
            else if (cmd == "leave")
            {
                Associate select = databases.Associates.FirstOrDefault(Associate => Associate.ActId == id && Associate.UserId == User.UserId);
                databases.Associates.Remove(select);
                databases.SaveChanges();
                return Redirect("/home");
            }
            return Redirect($"/activity/{id}");
        }

    }
}
