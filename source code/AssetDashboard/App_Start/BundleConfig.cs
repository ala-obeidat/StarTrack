using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace StarTrack.Dashboard {

    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {

            var scriptBundle = new ScriptBundle("~/Scripts/bundle");
            var styleBundle = new StyleBundle("~/Content/bundle");

            // jQuery
            scriptBundle
                .Include("~/Scripts/jquery-3.4.1.js");

            // Bootstrap
            scriptBundle
                .Include("~/Scripts/bootstrap.js");

            scriptBundle.Include("~/Scripts/main.js");
            scriptBundle.Include("~/Content/dialog/js/alertify.js");
            scriptBundle.Include("~/Content/dialog/js/Dialog.js");
            scriptBundle.Include("~/Content/fontawesome/js/all.min.js"); 
            scriptBundle.Include("~/Content/data-table/js/Grid.js");
            scriptBundle.Include("~/Content/data-table/js/grid.partial.js");
            scriptBundle.Include("~/Content/data-table/js/jquery.dataTables.js");

            // Bootstrap
            styleBundle
                .Include("~/Content/bootstrap.css");

            // Custom site styles
            styleBundle
                .Include("~/Content/Site.css");

            styleBundle.Include("~/Content/dialog/css/alertify.css");
            styleBundle.Include("~/Content/data-table/css/jquery.dataTables.css");
            styleBundle.Include("~/Content/data-table/css/grid.ltr.css");
            styleBundle.Include("~/Content/fontawesome/css/all.min.css");
            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}