// Copyright (c) 2016 Will Blankenship, All Rights Reserved.

using System.Web.Mvc;
using System.Web.Routing;

namespace GBG.Web
{
   public class RouteConfig
   {
      public static void RegisterRoutes(RouteCollection routes)
      {
         routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

         routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
      }
   }
}