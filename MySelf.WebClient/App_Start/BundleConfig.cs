﻿using System.Web;
using System.Web.Optimization;

namespace MySelf.WebClient
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = false;   //enable CDN support

            //bundles.Add(new ScriptBundle("~/bundles/raphael",
            //    "//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js").Include(
            //    "~/Scripts/raphael-min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqplot")
            //    .Include("~/Scripts/jquery.jqplot.min.js")
            //    .Include("~/Scripts/jplotplugins/jqplot.highlighter.min.js")
            //    .Include("~/Scripts/jplotplugins/jqplot.cursor.min.js")
            //    .Include("~/Scripts/jplotplugins/jqplot.dateAxisRenderer.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery")
            //    .Include("~/Scripts/jquery-2.0.3.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jslibs")
            //                .Include("~/Scripts/toastr.min.js")
            //                .Include("~/Scripts/moment.min.js")
            //    );

            //bundles.Add(new ScriptBundle("~/bundles/prologue")
            //                .Include("~/Scripts/skel.min.js")
            //                .Include("~/Scripts/skel-panels.min.js")
            //                .Include("~/Scripts/init.js")
            //    );

            //bundles.Add(new ScriptBundle("~/bundles/ajaxlogin").Include(
            //    "~/app/js/ajaxlogin.js"));

            bundles.Add(new ScriptBundle("~/bundles/superciccio")
                            .Include("~/Scripts/toastr.js")
                            .Include("~/Scripts/moment.js")
                            .Include("~/Scripts/jquery-ui-{version}.js")
                            .Include("~/Scripts/raphael-min.js")
                            .Include("~/Scripts/morris.min.js")
                            .Include("~/Scripts/ajaxlogin.js")
                            .Include("~/Scripts/bootstrap.min.js")
                            .Include("~/Scripts/bootstrap-datepicker.min.js")
                            .Include("~/Scripts/bootstrap-datetimepicker.min.js")
                            .Include("~/Scripts/angular.min.js")
                            .Include("~/Scripts/angular-resource.js")
                            .Include("~/Scripts/angular-route.js")
                            .Include("~/Scripts/angular-animate.js")
                            .Include("~/Scripts/angular-ui/ui-bootstrap.js")
                            );

            //bundles.Add(new ScriptBundle("~/bundles/angular")
            //                .Include("~/Scripts/angular.js")
            //                .Include("~/Scripts/angular-resource.js")
            //                .Include("~/Scripts/angular-route.js")
            //                .Include("~/Scripts/angular-animate.js")
            //                .Include("~/Scripts/angular-ui/ui-bootstrap.js")
            //                //.Include("~/Scripts/ui-bootstrap-0.6.0.min.js")
            //                //.Include("~/Scripts/ui-bootstrap-tpls-0.6.0.min.js")
            //                //.Include("~/Scripts/ng-table.js")
            //                );

            bundles.Add(new ScriptBundle("~/bundles/myselflog")
                .Include("~/app/js/*.js")
                .Include("~/app/js/services/*.js")
                .Include("~/app/js/controllers/*.js")
                );

            //bundles.Add(new ScriptBundle("~/bundles/jquerymobile")
            //    .Include("~/Scripts/jquery.mobile*"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui")
            //    .Include("~/Scripts/jquery-ui-{version}.js")
            //    //.Include("~/Scripts/dateTimePicker.js")
            //    );

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/morriscss",
                "http://cdnjs.cloudflare.com/ajax/libs/morris.js/0.5.1/morris.css").Include("~/Content/morris.css"));
            //"http://cdn.oesmith.co.uk/morris-0.4.3.min.css").Include("~/Content/morris-0.4.3.min.css"));

            bundles.Add(new StyleBundle("~/Content/jqplotcss")
                .Include("~/Content/jquery.jqplot.css"));

            bundles.Add(new StyleBundle("~/Content/mobilecss")
                .Include("~/Content/jquery.mobile*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap/bootstrap.min"));

            bundles.Add(new StyleBundle("~/Content/myCss")
                .Include("~/Content/myStyle.css")
                );

            bundles.Add(new StyleBundle("~/Content/noscriptprologuecss")
                .Include("~/Content/skel-noscript.css")
                .Include("~/Content/style.css")
                .Include("~/Content/style-wide.css")
                );

            bundles.Add(new StyleBundle("~/Content/css4libs")
                .Include("~/Content/toastr.min.css")
                .Include("~/Content/morris-0.4.3.min.css")
                .Include("~/Content/ng-table.css")
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