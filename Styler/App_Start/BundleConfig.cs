using System.Web;
using System.Web.Optimization;

namespace Styler
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/bootbox.min.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/chosen.jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-yeti.min.css",
                      "~/Content/site.css",
                      "~/Content/uknow.css"
                      ));
            bundles.Add(new StyleBundle("~/AdminContent/css").Include(
                     "~/Content/bootstrap-yeti.min.css",
                     "~/Content/site.css",
                     "~/Content/toastr.min.css",
                     "~/Content/chosen.min.css"));
        }
    }
}
