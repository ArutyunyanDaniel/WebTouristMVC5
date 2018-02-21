using System.Web;
using System.Web.Optimization;

namespace WebTourist
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/_Site.css",
                      "~/Content/_Preloader.css"));

            bundles.Add(new ScriptBundle("~/bundles/myscripts").Include(
                        "~/Scripts/MyScripts/Helper.js",
                        "~/Scripts/MyScripts/AjaxRequest.js",
                        "~/Scripts/MyScripts/Map.js",
                        "~/Scripts/MyScripts/AjaxEvent.js"));

            bundles.Add(new ScriptBundle("~/bundles/markerCluster").Include(
                        "~/Scripts/markerclusterer.js"));
        }
    }
}
