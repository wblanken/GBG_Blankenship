// Copyright (c) 2016 Will Blankenship, All Rights Reserved.

using System.Web.Http;

namespace GBG.Web
{
   public static class WebApiConfig
   {
      public static void Register(HttpConfiguration config)
      {
         // Web API configuration and services

         // Web API routes
         config.MapHttpAttributeRoutes();

         config.Routes.MapHttpRoute(
            name: "SampleApi",
            routeTemplate: "api/samples/",
            defaults: new {controller = "samples"});

         config.Routes.MapHttpRoute(
            name: "StatusApi",
            routeTemplate: "api/status/",
            defaults: new { controller = "status" });

         config.Routes.MapHttpRoute(
            name: "UserApi",
            routeTemplate: "api/users/",
            defaults: new { controller = "user" });

         config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new {id = RouteParameter.Optional}
            );
      }
   }
}