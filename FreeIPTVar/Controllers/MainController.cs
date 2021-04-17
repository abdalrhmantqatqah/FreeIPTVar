using FreeIPTVar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeIPTVar.Controllers
{
    public class MainController : Controller
    {
        FreeIPTVarEntities db = new FreeIPTVarEntities();

        // GET: Main
        public ActionResult Index()
        {
            List<Category> MyList = db.Categories.ToList();

            return View(MyList);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            UserModel model = new UserModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Registration(UserModel model)
        {
            if (ModelState.IsValid)
            {
                User oldRecord = db.Users.Where(x => x.UserName == model.UserName ).FirstOrDefault();

                if (oldRecord == null)
                {
                    User newuser = new User();
                    newuser.UserName = model.UserName;
                    newuser.Password = model.Password;
                    newuser.UserType = "Admin";
                    db.Users.Add(newuser);
                    db.SaveChanges();

                    return RedirectToAction("Login");

                }
                else
                {
                    ViewBag.ErrorMsg = "user already taken";
                    return View(model);
                }

            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            UserModel model = new UserModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {

            User user = db.Users.Where(x => x.UserName == model.UserName && x.Password == model.Password).FirstOrDefault();

            if (user != null)
            {
                Session["UserInfo"] = user;
                if (user.UserType == "Normal")
                {
                    return RedirectToAction("Index");
                }
                else//Admin
                {
                    return RedirectToAction("Index", "Category");
                }
            }

            //model.ErrorMsg = "User Name or Password wrong !";
            ViewBag.ErrorMsg = "User Name or Password wrong !";
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["UserInfo"] = null;
            return RedirectToAction("Index", "Main");
        }

        [HttpGet]
        public ActionResult ServerInfo(int ServerID)
        {
            Server model = db.Servers.Where(x => x.ServerID == ServerID).FirstOrDefault();
            return View(model);
        }

    }
}