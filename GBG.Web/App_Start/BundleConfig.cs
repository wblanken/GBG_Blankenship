// Copyright (c) 2016 Will Blankenship, All Rights Reserved.

using System.Web.Optimization;

namespace GBG.Web
{
   public class BundleConfig
   {
      public static void RegisterBundles(BundleCollection bundles)
      {
         // 3rd Party libraries
         bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
            "~/Scripts/Vendors/angular.min.js",
            "~/Scripts/Vendors/angular-route.min.js",
            "~/Scripts/Vendors/bootstrap.min.js",
            "~/Scripts/Vendors/jquery-1.9.1.min.js"
            ));

         // Bootstrap styles
         bundles.Add(new StyleBundle("~/Content/css").Include(
            "~/Content/site.css",
            "~/Content/Bootstrap/bootstrap.min.css",
            "~/Content/Bootstrap/bootstrap-theme.min.css"
            ));

         // My App
         bundles.Add(new ScriptBundle("~/bundles/app").Include(
            "~/Scripts/app/app.js",
            "~/Scripts/app/Service/sampleService.js",
            "~/Scripts/app/Service/statusService.js",
            "~/Scripts/app/Service/userService.js",
            "~/Scripts/app/Samples/sampleCtrl.js",
            "~/Scripts/app/Samples/viewSamples.directive.js",
            "~/Scripts/app/Samples/addSample.directive.js"
            ));
      }
   }
}