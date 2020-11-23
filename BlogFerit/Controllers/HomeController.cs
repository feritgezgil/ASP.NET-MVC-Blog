using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogFerit.BL;
using BlogFerit.DAL.EF;
using BlogFerit.DataEntities;
using BlogFerit.Models;

namespace BlogFerit.Controllers
{
    public class HomeController : Controller
    {
        ArticleManager repoArticle = new ArticleManager();
        AuthorManager repoAuthor = new AuthorManager();
        SeoManager repoSEO = new SeoManager();
        DataContext db = new DataContext();

        //GET: Home
        public ActionResult Index()
        {
            /*
            Bu kod veri tabanına ilgili tabloların oluşturulup oluşturulmadığını test için eklendi
            Çalıştırıldığında Veritabanı silinerek yeniden oluşturuluyor.
            Bu sayede veritabanı değişikliği daha hızlı gerçekleşiyor.
            Test test = new Test();
             */

            //Aşağıdaki Kodlar İle SEO bilgilerini Dinamik olarak Oluşturduk.
            var articleList = db.article.Where(x => x.IsActive == true && x.IsDelete == false); // .Take(10) eklenebilir?
            var SeoInfo = repoSEO.List().FirstOrDefault();
            ViewBag.Keywords = SeoInfo.Keywords;
            ViewBag.Description = SeoInfo.Description;
            ViewBag.Title = SeoInfo.Title;
            ViewBag.Logo = SeoInfo.LogoImage;
            ViewBag.Favicon = SeoInfo.Favicon;




            return View(articleList);
 
        }


        public ActionResult Advertisement()
        {
            return View();
        }

        public ActionResult Categories()
        {
            var categories = db.categories.Where(cat => cat.IsDelete == false);
            return View(categories);
        }

    


        public ActionResult Detail(string linkUrl = "")
        {
            if (linkUrl == "")
            {
                return RedirectToAction("", "");
            }


            ViewBag.Title = "Makale | Detay Sayfasi";
            ArticleViewModel articleModel = (from article in repoArticle.List()
                                             join author in repoAuthor.List() on article.Author equals author.NameSurname
                                             where article.ArticleUrl == linkUrl
                                             select new ArticleViewModel
                                             {
                                                 //Article
                                                 ArticleUrl = article.ArticleUrl,
                                                 ArticleCategory = article.CategoryName,
                                                 ArticleDate = article.ArticleDate,
                                                 Content = article.Content,
                                                 Title = article.Title,
                                                 ArticleReading = article.ReadingCount,
                                                 ArticleTags = article.Tags.Split(','),
                                                 // Author 
                                                 AuthorAbout = author.AuthorAbout,
                                                 AuthorGithub = author.GithubUrl,
                                                 AuthorLinkedin = author.LinkedinUrl,
                                                 AuthorFacebook = author.FacebookUrl,
                                                 AuthorImageUrl = author.ImageUrl,
                                                 AuthorName = author.NameSurname,
                                                 AuthorTwitter = author.TwitterUrl
                                             }).FirstOrDefault();

            if (articleModel == null)
            {
                return RedirectToAction("", "");
            }

            try
            {
                ViewBag.Title = articleModel.Title;
                ViewBag.Keywords = String.Join(",", articleModel.ArticleTags);
                ViewBag.Description = articleModel.Content.Substring(0, 250);

                var SeoInfo = repoSEO.List().FirstOrDefault();
                ViewBag.Logo = SeoInfo.LogoImage;
                ViewBag.Favicon = SeoInfo.Favicon;
            }
            catch (Exception)
            {

            }

            var sArticle = db.article.Where(art => art.ArticleUrl == linkUrl).FirstOrDefault();
            sArticle.ReadingCount += 1;
            db.SaveChanges();

            return View(articleModel);
        }

        public ActionResult TopArticle()
        {
            var articleList = repoArticle.List().OrderByDescending(m => m.ReadingCount).Take(3).ToList();
            return View(articleList);
        }





    }
}