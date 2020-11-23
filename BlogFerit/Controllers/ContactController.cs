using BlogFerit.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFerit.Controllers
{
    public class ContactController : Controller
    {

        SeoManager repoSEO = new SeoManager();
        AuthorManager repoAuthor = new AuthorManager();

        // GET: Contacr
        public ActionResult Index()
        {
            //Aşağıdaki Kodlar İle SEO bilgilerini Dinamik olarak Oluşturduk.
            var SeoInfo = repoSEO.List().FirstOrDefault();
            ViewBag.Keywords = SeoInfo.Keywords;
            ViewBag.Description = SeoInfo.Description;
            ViewBag.Title = "İleşim";
            ViewBag.Logo = SeoInfo.LogoImage;
            ViewBag.Favicon = SeoInfo.Favicon;

            var yazar = repoAuthor.List().FirstOrDefault();
            return View(yazar);
        }
    }
}