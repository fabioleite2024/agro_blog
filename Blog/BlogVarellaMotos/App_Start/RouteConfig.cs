using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogVarellaMotos
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //routes.Add("BlogPost", new SeoFriendlyRoute("Blog/{slugTitle}",
            //    new RouteValueDictionary(new { controller = "Blog", action = "Details" }),
            //    new MvcRouteHandler()));

            routes.MapRoute(
               name: "BlogPost",
               url: "artigo/{slugTitle}",
               defaults: new { controller = "Blog", action = "Artigo", slugTitle= UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Admin",
              url: "Admin/{action}/{id}",
              defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
           );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
