using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogFerit
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "AdminPanel",
              url: "Admin/{action}/{id}",
              defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Login",
               url: "Security/Login",
               defaults: new { controller = "Security", action = "Login" }
           );

            routes.MapRoute(
              name: "Logout",
              url: "Security/Logout",
              defaults: new { controller = "Security", action = "Logout" }
          );

            routes.MapRoute(
             name: "Hakkimda",
             url: "About",
             defaults: new { controller = "About", action = "Index", String = "" }
              );

            routes.MapRoute(
            name: "Iletisim",
            url: "Contact",
            defaults: new { controller = "Contact", action = "Index", String = "" }
            );

            routes.MapRoute(
               name: "ArticleDetail",
               url: "{CategoryName}/{linkUrl}",
               defaults: new { controller = "Home", action = "Detail", String = "" }
           );
            routes.MapRoute(
             name: "Category",
             url: "{CategoryName}",
             defaults: new { controller = "Category", action = "Index", String = "" }
         );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
