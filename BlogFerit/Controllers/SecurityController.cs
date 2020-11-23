using BlogFerit.BL;
using BlogFerit.DAL.EF;
using BlogFerit.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogFerit.Controllers
{
    public class SecurityController : Controller
    {
        // GET: User
        string password = "Ferit Gezgil";

        public ActionResult Index()
        {
            return View();
        }

        // GET: Security
        DataContext db = new DataContext();

        [HttpGet]
        public ActionResult login()
        {
            ViewBag.Uyari = "Giriş Yapınız";
            return View();
        }

   
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Author author)
        {
            string EncText = Security.Encrypt(author.Password, password);

            var userInDb = db.authors.FirstOrDefault(x => x.Email == author.Email && x.Password == EncText);
            if (userInDb !=null)
            {
                FormsAuthentication.SetAuthCookie(userInDb.Email, false);
                return RedirectToAction("Index","Admin");
            }
            else
            {
                ViewBag.Uyari = "Giriş Bilgileriniz Hatalı";
                return View();
            }
         
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }



    }
}