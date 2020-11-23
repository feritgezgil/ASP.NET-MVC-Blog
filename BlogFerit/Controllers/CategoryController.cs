using BlogFerit.BL;
using BlogFerit.DAL.EF;
using BlogFerit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFerit.Controllers
{
    public class CategoryController : Controller
    {
        DataContext db = new DataContext();
        ArticleManager repoArticle = new ArticleManager();
        SeoManager repoSEO = new SeoManager();

        public ActionResult Index(string CategoryName)
        {
            if (CategoryName == null)
            {
                return RedirectToAction("","");
            }

            //Aşağıdaki Kodlar İle SEO bilgilerini Dinamik olarak Oluşturduk.
            var articleList = db.article.Where(x => x.IsActive == true && x.IsDelete == false && x.CategoryName == CategoryName); // .Take(10) eklenebilir?
            var SeoInfo = repoSEO.List().FirstOrDefault();
            ViewBag.Keywords = SeoInfo.Keywords;
            ViewBag.Description = SeoInfo.Description;
            ViewBag.Title = SeoInfo.Title + " | " + CategoryName;
            ViewBag.Logo = SeoInfo.LogoImage;
            ViewBag.Favicon = SeoInfo.Favicon;

            return View(articleList);
        }
    }
}