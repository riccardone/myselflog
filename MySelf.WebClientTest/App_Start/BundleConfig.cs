using System.Web.Optimization;

namespace MySelf.WebClientTest.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery",
                "http://codeorigin.jquery.com/jquery-2.0.3.min.js").Include(
                "~/Scripts/jquery-2.0.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/d3",
                "http://d3js.org/d3.v3.min.js").Include(
                "~/Scripts/d3.v3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                            .Include("~/Scripts/angular.min.js")
                            .Include("~/Scripts/angular-resource.min.js")
                            .Include("~/Scripts/ui-bootstrap-0.6.0.min.js")
                            .Include("~/Scripts/ui-bootstrap-tpls-0.6.0.min.js")
                            .Include("~/Scripts/angular-strap.min.js")
                            .Include("~/Scripts/ng-grid-2.0.7.min.js")
                            .Include("~/Scripts/line-chart.min.js")
                            );

            bundles.Add(new ScriptBundle("~/bundles/myselflog")
                .Include("~/app/js/myselflog.main.js")
                .Include("~/app/js/services/myselflog.resource.js")
                .Include("~/app/js/controllers/test.controller.js")
                .Include("~/app/js/controllers/chart.controller.js")
                );

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}