using LanguageChange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LanguageChange.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                using (UserDBContext db = new UserDBContext())
                {
                    var user = (from u in db.users
                                select u)
                                .Where(u => u.Login == model.Login && u.Password == model.Password)
                                .FirstOrDefault();

                    if(user == null)
                    {
                        ModelState.AddModelError("", "Данные указаны неверно");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(model.ID.ToString(), true);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}