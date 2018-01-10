﻿using System.Web.Optimization;

namespace GigHubWebApp {
    public class BundleConfig {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app/services/attendanceService.js",
                "~/Scripts/app/controller/gigsController.js",
                "~/Scripts/app/services/followingService.js",
                "~/Scripts/app/controller/gigDetailsController.js",
                "~/Scripts/app/app.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js"
                        , "~/Scripts/underscore.min.js"
                        , "~/Scripts/moment.min.js"
                        , "~/Scripts/bootstrap.js"
                        , "~/Scripts/respond.js"
                        , "~/Scripts/bootbox.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/animate.min.css"));
        }
    }
}