using System.Web;
using System.Web.Optimization;

namespace Repositorio.Cliente
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

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
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular/angular.min.js",
                      "~/Scripts/angular/angular-animate.min.js",
                      "~/Scripts/angular/angular-aria.min.js",
                      "~/Scripts/angular/angular-ui-router.min.js",
                      "~/Scripts/angular/angular-material.min.js",
                      "~/Scripts/angular/angular-resource.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ace").Include(
                      "~/Scripts/ace/src-noconflict/ace.js",
                      "~/Scripts/ace/ui-ace.js"));

            bundles.Add(new ScriptBundle("~/bundles/zip").Include(
                      "~/Scripts/jszip/jszip-utils.js",
                      "~/Scripts/jszip/jszip.js"));

            bundles.Add(new ScriptBundle("~/bundles/repositorio").Include(
                      "~/repositorio/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/repositorioServices").Include(
                      "~/repositorio/services/apiService.js",
                      "~/repositorio/services/EditorIdService.js"));

            bundles.Add(new ScriptBundle("~/bundles/repositorioControllers").Include(
                      "~/repositorio/controllers/AddSnippetCtrl.js",
                      "~/repositorio/controllers/listaMenu-controller.js",
                      "~/repositorio/controllers/search-controller.js",
                      "~/repositorio/controllers/sideBar-controller.js",
                      "~/repositorio/controllers/SnippetsCtrl.js",
                      "~/repositorio/controllers/VisualizarSnippetCtrl.js",
                      "~/repositorio/controllers/EditorCtrl.js"));

        }
    }
}
