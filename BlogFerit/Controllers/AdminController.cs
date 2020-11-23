using BlogFerit.BL;
using BlogFerit.DAL.EF;
using BlogFerit.DataEntities;
using BlogFerit.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogFerit.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        DataContext db = new DataContext();
        ArticleManager repoArticle = new ArticleManager();
        AuthorManager repoAuthor = new AuthorManager();
        SeoManager repoSeo= new SeoManager();
        string password = "Ferit Gezgil"; //Sha-256 kullanılıyor;
        // GET: ArticleAdmin

 
        public ActionResult Index()
        {
            var siteSeo = repoSeo.List().FirstOrDefault();
            var user = repoAuthor.List().FirstOrDefault();
            ViewBag.Favicon = siteSeo.Favicon;
            ViewBag.UserName = user.NameSurname;
            ViewBag.UserImage = user.ImageUrl;
            ViewBag.About = user.AuthorAbout;

            var model = db.article.Where(x => x.IsDelete == false) ;
            return View(model);
        }


   
        public ActionResult Trash()
        {
            var siteSeo = repoSeo.List().FirstOrDefault();
            var user = repoAuthor.List().FirstOrDefault();
            ViewBag.Favicon = siteSeo.Favicon;
            ViewBag.UserName = user.NameSurname;
            ViewBag.UserImage = user.ImageUrl;
            ViewBag.About = user.AuthorAbout;

            var model = db.article.Where(x => x.IsDelete == true);
            return View(model);
        }

  
        public ActionResult Categories()
        {
            var siteSeo = repoSeo.List().FirstOrDefault();
            var user = repoAuthor.List().FirstOrDefault();
            ViewBag.Favicon = siteSeo.Favicon;
            ViewBag.UserName = user.NameSurname;
            ViewBag.UserImage = user.ImageUrl;
            ViewBag.About = user.AuthorAbout;
            var model = db.categories;
            return View(model);
        }

 
        public ActionResult DeleteArticle(int id)
        {
            var model = repoArticle.GeyById(id);
            model.IsDelete = true;
            repoArticle.Update(model);
            return RedirectToAction("Index","Admin");
        }
   
        public ActionResult UnDeleteArticle(int id)
        {
            var model = repoArticle.GeyById(id);
            model.IsDelete = false;
            repoArticle.Update(model);
            return RedirectToAction("Trash");
        }

    
        public ActionResult deleteTrash(string check)
        {
            if (check == "true")
            {
                var model = db.article.Where(x=> x.IsDelete == true).ToList();

                foreach (var item in model)
                {
                    db.article.Remove(item);
                
                }
                db.SaveChanges();
            }

            return RedirectToAction("Trash");
        }

  
        public ActionResult showArticle(int id)
        {
            var model = repoArticle.GeyById(id);
            model.IsActive = true;
            repoArticle.Update(model);
            return RedirectToAction("Index");
        }
  
        public ActionResult hideArticle(int id)
        {
            var model = repoArticle.GeyById(id);
            model.IsActive = false;
            repoArticle.Update(model);
            return RedirectToAction("Index");
        }

      
        public ActionResult CreateArticle()
        {
            var siteSeo = repoSeo.List().FirstOrDefault();
            var user = repoAuthor.List().FirstOrDefault();
            ViewBag.Favicon = siteSeo.Favicon;
            ViewBag.UserName = user.NameSurname;
            ViewBag.UserImage = user.ImageUrl;
            ViewBag.About = user.AuthorAbout;
            ViewBag.Title = "Yazı | Yeni Ekle";
            var model = repoAuthor.List().FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateArticle(Article article, HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                string guide = Guid.NewGuid().ToString();
                string filePath = Path.Combine(Server.MapPath("~/images/"), guide + "_" + Path.GetFileName(postedFile.FileName));
                string fileSavePath = "images/" + guide + "_" + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                article.ImageUrl = fileSavePath;
            }

            article.ArticleDate = DateTime.Now.ToString("dd MMMM yyyy");
            article.ReadingCount = 0;
            article.IsActive = true;
            article.IsDelete = false;
            article.ArticleUrl = Utils.UrlDuzenleme.UrlCevir(article.Title).ToLower();
            db.article.Add(article);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }


        public ActionResult EditArticle(int id)
        {

            var siteSeo = repoSeo.List().FirstOrDefault();
            var user = repoAuthor.List().FirstOrDefault();
            ViewBag.Favicon = siteSeo.Favicon;
            ViewBag.UserName = user.NameSurname;
            ViewBag.UserImage = user.ImageUrl;
            ViewBag.About = user.AuthorAbout;

            ViewBag.Title = "Yazı | Yeni Ekle";

            var model = repoArticle.GeyById(id);

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult EditArticle(Article article, HttpPostedFileBase postedFile)
        {

            if (postedFile != null)
            {
                string guide = Guid.NewGuid().ToString();
                string filePath = Path.Combine(Server.MapPath("~/images/"), guide + "_" + Path.GetFileName(postedFile.FileName));
                string fileSavePath = "images/" + guide + "_" + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                article.ImageUrl = fileSavePath;
            }

   
            article.ArticleUrl = Utils.UrlDuzenleme.UrlCevir(article.Title).ToLower();
            var nArticle = db.article.Where(x=> x.Id == article.Id).FirstOrDefault();

            nArticle.Title = article.Title;
            nArticle.Content = article.Content;
            nArticle.ImageUrl = article.ImageUrl;
            nArticle.CategoryName = article.CategoryName;
            nArticle.Tags = article.Tags;
            db.SaveChanges();

            return RedirectToAction("Index");

        }

 
        public ActionResult EditCategory(int id)
        {

            var siteSeo = repoSeo.List().FirstOrDefault();
            var user = repoAuthor.List().FirstOrDefault();
            ViewBag.Favicon = siteSeo.Favicon;
            ViewBag.UserName = user.NameSurname;
            ViewBag.UserImage = user.ImageUrl;
            ViewBag.About = user.AuthorAbout;

            ViewBag.Title = "Yazı | Yeni Ekle";

            var model = db.categories.Where(x=> x.Id == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult EditCategory(Categories categories)
        {
            var nCategory = db.categories.Where(x => x.Id == categories.Id).FirstOrDefault();
            //Bu kategorideki yazıların bilgilerinide güncelle
            var Articles = db.article.Where(art => art.CategoryName == nCategory.CategoryName).ToList();
            foreach (var item in Articles)
            {
                item.CategoryName = categories.CategoryName;
            }

            nCategory.CategoryName = categories.CategoryName;
            nCategory.IsDelete = categories.IsDelete;

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult showCategory(int id)
        {
            Categories model = db.categories.Where(cat => cat.Id == id).FirstOrDefault();
            model.IsDelete = false;
            db.SaveChanges();
            return RedirectToAction("Categories");
        }


        public ActionResult hideCategory(int id)
        {
            Categories model = db.categories.Where(cat=> cat.Id == id).FirstOrDefault();
            model.IsDelete = true;
            db.SaveChanges();
            return RedirectToAction("Categories");
        }
   
 
        public ActionResult AdminUpdate()
        {
            var siteSeo = repoSeo.List().FirstOrDefault();
            var user = db.authors.FirstOrDefault();
            ViewBag.Favicon = siteSeo.Favicon;
            ViewBag.UserName = user.NameSurname;
            ViewBag.UserImage = user.ImageUrl;
            ViewBag.About = user.AuthorAbout;
            string Dencr = Security.Decrypt(user.Password, password);
            user.Password = Dencr;
            ViewBag.Title = "Admin Ayarları";
            return View(user);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AdminUpdate(Author authors, HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                string guide = Guid.NewGuid().ToString();
                string filePath = Path.Combine(Server.MapPath("~/images/"), guide + "_" + Path.GetFileName(postedFile.FileName));
                string fileSavePath = "images/" + guide + "_" + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                authors.ImageUrl = fileSavePath;
            }
            Author nArthour = db.authors.Where(usr=> usr.Id == authors.Id).FirstOrDefault();
            string Dencr = Security.Encrypt(authors.Password, password);
            nArthour.NameSurname = authors.NameSurname;
            nArthour.AuthorAbout = authors.AuthorAbout;
            nArthour.ImageUrl = authors.ImageUrl;
            nArthour.Email = authors.Email;
            nArthour.Password = Dencr;
            nArthour.GithubUrl = authors.GithubUrl;
            nArthour.LinkedinUrl = authors.LinkedinUrl;
            nArthour.FacebookUrl = authors.FacebookUrl;
            nArthour.TwitterUrl = authors.TwitterUrl;
            nArthour.IsActive = authors.IsActive;
            nArthour.IsDelete = authors.IsDelete;
            nArthour.Role = authors.Role;
            nArthour.Phone = authors.Phone;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult SeoUpdate()
        {
            var siteSeo = repoSeo.List().FirstOrDefault();
            var user = db.authors.FirstOrDefault();
            ViewBag.Favicon = siteSeo.Favicon;
            ViewBag.UserName = user.NameSurname;
            ViewBag.UserImage = user.ImageUrl;
            ViewBag.About = user.AuthorAbout;
            ViewBag.Title = "Seo Ayarları";
            return View(siteSeo);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SeoUpdate(SeoSettings seoSettings, HttpPostedFileBase postedFile, HttpPostedFileBase postedFile2)
        {
            if (postedFile != null)
            {
                string guide = Guid.NewGuid().ToString();
                string filePath = Path.Combine(Server.MapPath("~/images/"), guide + "_" + Path.GetFileName(postedFile.FileName));
                string fileSavePath = "images/" + guide + "_" + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                seoSettings.LogoImage = fileSavePath;
            }
            if (postedFile2 != null)
            {
                string guide = Guid.NewGuid().ToString();
                string filePath = Path.Combine(Server.MapPath("~/images/"), guide + "_" + Path.GetFileName(postedFile2.FileName));
                string fileSavePath = "images/" + guide + "_" + Path.GetFileName(postedFile2.FileName);
                postedFile2.SaveAs(filePath);
                seoSettings.Favicon = fileSavePath;
            }
            SeoSettings nSeo = db.seoSettings.Where(seo => seo.Id == seoSettings.Id).FirstOrDefault();
            nSeo.Keywords = seoSettings.Keywords;
            nSeo.Description = seoSettings.Description;
            nSeo.Title = seoSettings.Title;
            nSeo.LogoImage = seoSettings.LogoImage;
            nSeo.Favicon = seoSettings.Favicon;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddCategory(Categories categories)
        {
            categories.IsDelete = false;
            db.categories.Add(categories);
            db.SaveChanges();
            return RedirectToAction("Categories");
        }
      
    }
}