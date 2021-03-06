﻿using System.Web;
using System.Web.Optimization;

namespace Repositorio.Cliente
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/materialize/materialize.js"));

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
                      "~/Content/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular/angular.min.js",
                      "~/Scripts/angular/angular-animate.min.js",
                      "~/Scripts/angular/angular-aria.min.js",
                      "~/Scripts/angular/angular-ui-router.js",
                      "~/Scripts/angular/angular-material.min.js",
                      "~/Scripts/angular/angular-resource.min.js",
                      "~/Scripts/angular/angular-material-icons.min.js",
                      "~/Scripts/angular/svg-morpheus.js"));

            bundles.Add(new ScriptBundle("~/bundles/ace").Include(
                      "~/Scripts/ace/src-noconflict/ace.js",
                      "~/Scripts/ace/ui-ace.js"));

            bundles.Add(new ScriptBundle("~/bundles/zip").Include(
                      "~/Scripts/jszip/jszip-utils.js",
                      "~/Scripts/jszip/jszip.js",
                      "~/Scripts/jszip/FileSaver.js"));

            bundles.Add(new ScriptBundle("~/bundles/repositorio").Include(
                      "~/repositorio/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/repositorioConfig").Include(
                      "~/repositorio/appConfig.js"));

            bundles.Add(new ScriptBundle("~/bundles/repositorioServices").Include(
                      "~/repositorio/services/apiService.js",
                      "~/repositorio/services/projetosService.js",
                      "~/repositorio/services/clientesService.js",
                      "~/repositorio/services/areasService.js"));

            bundles.Add(new ScriptBundle("~/bundles/repositorioControllers").Include(
                      "~/repositorio/controllers/AddProjetoCtrl.js",
                      "~/repositorio/controllers/AddSnippetCtrl.js",
                      "~/repositorio/controllers/LoginCtrl.js",
                      "~/repositorio/controllers/appCtrl.js",
                      "~/repositorio/controllers/SnippetsCtrl.js",
                      "~/repositorio/controllers/VisualizarSnippetCtrl.js",
                      "~/repositorio/controllers/EditorCtrl.js",
                      "~/repositorio/controllers/LeftCtrl.js",
                      "~/repositorio/controllers/materialTemplateCtrl.js",
                      "~/repositorio/controllers/angularTemplateCtrl.js"));
        }
    }
}
