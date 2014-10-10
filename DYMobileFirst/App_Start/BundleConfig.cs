using System.Web;
using System.Web.Optimization;

namespace DYMobileFirst
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/tinymceinit").Include(
"~/Scripts/tinymce/init.js"));

            bundles.Add(new ScriptBundle("~/bundles/ie").Include(
"~/js/ie/respond.min.js", "~/js/ie/html5.js", "~/js/ie/excanvas.js"));

            bundles.Add(new ScriptBundle("~/bundles/footable").Include(
            "~/js/footable.js", "~/js/footable.sort.js","~/js/footable.filter.js"));

            bundles.Add(new ScriptBundle("~/bundles/move").Include(
"~/js/movetop/move.js"));

            bundles.Add(new ScriptBundle("~/bundles/excel").Include(
"~/Scripts/excellentexport.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxvalidate").Include(
"~/Scripts/jquery.validate.js", "~/Scripts/jquery.unobtrusive-ajax.js", "~/Scripts/jquery.validate.unobtrusive.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/js/app.v2.js"));

            bundles.Add(new ScriptBundle("~/bundles/ui").Include(
           "~/js/jquery-ui-1.10.3.custom.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/touch").Include(
          "~/js/jquery.ui.touch-punch.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/fuelux").Include(
"~/js/fuelux/fuelux.js"));

            bundles.Add(new ScriptBundle("~/bundles/underscore").Include(
 "~/js/underscore-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
"~/js/datatables/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
"~/js/datepicker/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/slider").Include(
"~/js/slider/bootstrap-slider.js"));

            bundles.Add(new ScriptBundle("~/bundles/file-input").Include(
"~/js/file-input/bootstrap.file-input.js"));

            bundles.Add(new ScriptBundle("~/bundles/combodate").Include(
"~/js/combodate/combodate.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
"~/js/combodate/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/parsley").Include(
"~/js/parsley/parsley.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
"~/js/select2/select2.min.js"));



            bundles.Add(new ScriptBundle("~/bundles/flotmin").Include(
"~/js/charts/flot/jquery.flot.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/flottooltip").Include(
"~/js/charts/flot/jquery.flot.tooltip.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/flotresize").Include(
"~/js/charts/flot/jquery.flot.resize.js"));

            bundles.Add(new ScriptBundle("~/bundles/flotorderBars").Include(
"~/js/charts/flot/jquery.flot.orderBars.js"));

            bundles.Add(new ScriptBundle("~/bundles/flotpie").Include(
"~/js/charts/flot/jquery.flot.pie.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/flotdemo").Include(
"~/js/charts/flot/demo.js"));

            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
"~/js/fullcalendar/fullcalendar.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/prettyPhoto").Include(
"~/js/prettyphoto/jquery.prettyPhoto.js"));

            bundles.Add(new ScriptBundle("~/bundles/licious").Include(
"~/js/grid/jquery.grid-a-licious.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/gallery").Include(
"~/js/grid/gallery.js"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/css/font.css",
                      "~/css/app.v2.css"));

            bundles.Add(new StyleBundle("~/Content/select2").Include(
          "~/js/select2/select2.css"));

            bundles.Add(new StyleBundle("~/Content/footable").Include(
"~/css/footable.core.css", "~/css/footable.metro.css"));

            bundles.Add(new StyleBundle("~/Content/move").Include(
"~/js/movetop/move.css"));
        }
    }
}
