using System.Web;
using System.Web.Optimization;

namespace MySelf.WebClient
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;   //enable CDN support

            // //cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js
            // http://cdn.oesmith.co.uk/morris-0.4.3.min.js

            bundles.Add(new ScriptBundle("~/bundles/raphael",
                "//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js").Include(
                "~/Scripts/raphael-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/morris",
                "http://cdn.oesmith.co.uk/morris-0.4.3.min.js").Include(
                "~/Scripts/morris-0.4.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jslibs")
                            .Include("~/Scripts/jquery-{version}.js")
                            .Include("~/Scripts/toastr.min.js")
                            .Include("~/Scripts/moment.min.js")
                            );
                //.Include("~/Scripts/underscore-1.4.4.min.js")
                //.Include("~/Scripts/moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                            .Include("~/Scripts/angular.min.js")
                            .Include("~/Scripts/angular-resource.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/myselflog")
                .Include("~/app/js/myselflog.main.js")
                .Include("~/app/js/services/authentication.js")
                .Include("~/app/js/services/logger.js")
                .Include("~/app/js/services/myselflog.resource.js")
                .Include("~/app/js/services/logprofile.resource.js")
                .Include("~/app/js/services/securitylink.resource.js")
                .Include("~/app/js/services/myselflog.datacontext.js")
                .Include("~/app/js/services/user.resource.js")
                .Include("~/app/js/services/user.datacontext.js")
                .Include("~/app/js/services/friend.datacontext.js")
                .Include("~/app/js/controllers/login.controller.js")
                .Include("~/app/js/controllers/graph.controller.js")
                //.Include("~/app/js/controllers/log.controller.js")
                .Include("~/app/js/controllers/profile.controller.js")
                .Include("~/app/js/controllers/profiles.controller.js")
                .Include("~/app/js/controllers/newfriend.controller.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/friendlog")
                .Include("~/app/js/friend.main.js")
                .Include("~/app/js/services/logger.js")
                .Include("~/app/js/services/friend.datacontext.js")
                .Include("~/app/js/controllers/friend.controller.js")
                .Include("~/app/js/controllers/newfriend.controller.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/jquerymobile")
                .Include("~/Scripts/jquery.mobile*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui")
                .Include("~/Scripts/jquery-ui-{version}.js")
                .Include("~/Scripts/dateTimePicker.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/morriscss", "http://cdn.oesmith.co.uk/morris-0.4.3.min.css")
                .Include("~/Content/morris-0.4.3.min.css"));

            bundles.Add(new StyleBundle("~/Content/mobilecss").Include("~/Content/jquery.mobile*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap/bootstrap.min"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/site.css")
                .Include("~/Content/toastr.min.css")
                .Include("~/Content/myStyle.css")
                .Include("~/Content/ngi.css")
                );

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