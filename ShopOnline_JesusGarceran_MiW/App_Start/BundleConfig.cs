using System.Web;
using System.Web.Optimization;

namespace ShopOnline_JesusGarceran_MiW
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                      "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/price-range").Include(
                      "~/Scripts/price-range.js"));

            bundles.Add(new ScriptBundle("~/bundles/gmaps").Include(
                        "~/Scripts/gmaps.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.scrollUp").Include(
                    "~/Scripts/jquery.scrollUp.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.prettyPhoto").Include(
                    "~/Scripts/jquery.prettyPhoto.js"));

            bundles.Add(new ScriptBundle("~/bundles/fontawesome").Include(
                    "~/Scripts/fontawesome.js"));

            bundles.Add(new ScriptBundle("~/bundles/all").Include(
                    "~/Scripts/all.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/all.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/main.css",
                      "~/Content/prettyPhoto.css",
                      "~/Content/responsive.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/price-range.css",
                      "~/Content/animate.css"));
        }
    }
}
