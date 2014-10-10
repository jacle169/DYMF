using DYMobileFirst.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DYMobileFirst.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Logon(string name, string password, string ReturnUrl)
        {
            if (name == "aaa" && password == "aaa")
            {
                FormsAuthentication.SetAuthCookie(name, false);
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                return Redirect("/");
            }
            else
            {
                return Redirect("/Home/signin");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/signin");
        }

        public ActionResult buttons()
        {
            return View();
        }

        public ActionResult icons()
        {
            return View();
        }

        public ActionResult grid()
        {
            return View();
        }

        public ActionResult widgets()
        {
            return View();
        }

        public ActionResult components()
        {
            return View();
        }

        public ActionResult portlet()
        {
            return View();
        }

        public ActionResult list()
        {
            return View();
        }

        public ActionResult table()
        {
            return View();
        }

        public ActionResult form()
        {
            return View();
        }

        public ActionResult chart()
        {
            return View();
        }

        public ActionResult mail()
        {
            return View();
        }

        public ActionResult calendar()
        {
            return View();
        }

        public ActionResult timeline()
        {
            return View();
        }

        public ActionResult profile()
        {
            return View();
        }

        public ActionResult gallery()
        {
            return View();
        }

        public ActionResult invoice()
        {
            return View();
        }

        public ActionResult signin()
        {
            return View();
        }

        public ActionResult signup()
        {
            return View();
        }

        public ActionResult notfind()
        {
            return View();
        }

        public ActionResult docs()
        {
            return View();
        }

    }
}
