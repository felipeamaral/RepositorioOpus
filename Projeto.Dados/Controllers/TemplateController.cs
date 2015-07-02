using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ionic.Zip;
using Projeto.Dados.Models;
using System.IO;
using System.Net.Http.Headers;

namespace Projeto.Dados.Controllers
{
    public class TemplateController : ApiController
    {

        private string firstLettersUpper(string str)
        {
            string aux = str;
            string firstUpper = "";

            //Verifica se é um nome composto e transforma num nome só
            int espaco = aux.IndexOf(' ');
            while (espaco >= 0)
            {
                firstUpper += char.ToUpper(aux[0]) + aux.Substring(1, espaco - 1);
                aux = aux.Substring(espaco + 1);
                espaco = aux.IndexOf(' ');
            }
            firstUpper += char.ToUpper(aux[0]) + aux.Substring(1);

            return firstUpper;
        }

        // Retorna o template básico de .net com angular material
        [Route("api/template/material/{cor}/{rodape}")]
        public HttpResponseMessage GetMaterial(string cor, bool rodape, [FromUri] List<string> itens)
        {
            //Pega o arquivo zip que contem o template a ser utilizado
            ZipFile template = ZipFile.Read(System.Web.HttpContext.Current.Server.MapPath(
                "~/Arquivos/Templates/materialDesign.zip"));

            MemoryStream tmpRetorno = new MemoryStream();

            // Verifica se uma cor foi informada
            if (cor != null)
            {
                MemoryStream aux = new MemoryStream();
                string file = "";

                // Converte a stream do css em string pra poder fazer as modificações
                template["materialDesign/Content/Site.css"].Extract(aux);
                aux.Position = 0;
                var sr = new StreamReader(aux);
                file = sr.ReadToEnd();

                file = file + "\n\n/* Altera o layout das navs*/\r\nmd-toolbar.md-default-theme{\r\nbackground-color: #" + cor + ";\r\n}";

                // Exclui a entrada anterior e adiciona a nova
                template.RemoveEntry("materialDesign/Content/Site.css");
                template.AddEntry("materialDesign/Content/Site.css", file);
            }

            //Verifica se foram informados itens para o menu
            if (itens.Count > 0)
            {
                MemoryStream aux1 = new MemoryStream();
                string file1 = "";

                /* FAZ AS MUDANÇAS NO LAYOUT*/

                // Converte a stream do layout em string pra poder fazer as modificações
                template["materialDesign/Views/Shared/_Layout.cshtml"].Extract(aux1);
                aux1.Position = 0;
                var sr = new StreamReader(aux1);
                file1 = sr.ReadToEnd();

                string itensMenu = "<!-- Lista de itens do menu lateral -->\r\n<md-list>\r\n";
                string ctrl = "//Controla o icone das opções primarias do menu\r\n";

                foreach(string item in itens){

                    // Seta o que será colocado no layout
                    string firstUpper = firstLettersUpper(item);

                    // Coloca a primeira letra do firstUpper como minuscula
                    string firstLower = char.ToLower(firstUpper[0]) + firstUpper.Substring(1);

                    string options1 = "";
                    string options2 = "";

                    itensMenu += "<md-list-item ng-click=\"toggle" + firstUpper + "()\">\r\n" +
                            "<p> " + char.ToUpper(item[0]) + item.Substring(1) + " </p>\r\n" +
                            "<ng-md-icon icon=\"{{icon" + firstUpper + "}}\" style=\"fill: black\" class=\"md-icon-button\"></ng-md-icon>\r\n" +
                            "</md-list-item>\r\n";

                    if (item.Equals(itens[0])){
                        itensMenu += "<div ng-show=\"" + firstLower + "Options\" class=\"menu-secundario\">\r\n" +
                                        "<md-list-item ng-click=\"nothing()\">\r\n" +
                                        "<p> Menu secundario </p>\r\n" +
                                        "</md-list-item>\r\n" +
                                        "</div>\r\n";
                        options1 = "$scope." + firstLower + "Options = false;\r\n";
                        options2 = "$scope." + firstLower + "Options = $scope." + firstLower + "Options == true ? false : true;\r\n";
                    }

                    // Seta o que será colocado no controller
                    ctrl += "\r\n// Funções relacionadas ao item '" + firstUpper + "' do menu\r\n";
                    ctrl += "$scope.icon" + firstUpper +" = \"add\";\r\n" + 
                         "$scope.toggle" + firstUpper +" = function (esconde) {\r\n" +
                                "if (esconde) {\r\n" +
                                    "$scope.icon" + firstUpper +" = \"add\";\r\n" + options1 +
                                "} else {\r\n" +
                             "$scope.icon" + firstUpper +" = $scope.icon" + firstUpper + " == \"add\" ? \"remove\" : \"add\";\r\n" +
                             options2;

                    foreach (string it in itens){
                        if (!it.Equals(item)){
                            ctrl+= "$scope.toggle" + firstLettersUpper(it) + "(true);\r\n";
                        }
                    }
                    ctrl += "}\r\n};\r\n"; 
                }

                itensMenu += "</md-list>\r\n";


                file1 = file1.Replace("<!-- Lista de itens do menu lateral -->\r\n", itensMenu);

                // Verifica se o rodapé deve ser retirado
                if (rodape)
                {
                    string rod = "@RenderBody()\r\n\r\n" +
                                    "<!-- Rodapé -->\r\n" +
                                    "<md-content>\r\n" +
                                        "<div style=\"margin-top: 4em;\">\r\n" +
                                            "<md-toolbar class=\"navbar-fixed-bottom\">\r\n" +
                                                "<div layout=\"row\" layout-align=\"center center\" flex>\r\n" +
                                                    "<p><span>&copy;</span> @DateTime.Now.Year - footer</p>\r\n" +
                                                "</div>\r\n" +
                                            "</md-toolbar>\r\n" +
                                        "</div>\r\n" +
                                    "</md-content>\r\n";

                    file1 = file1.Replace("@RenderBody()", rod);
                }

                // Exclui a entrada anterior e adiciona a nova
                template.RemoveEntry("materialDesign/Views/Shared/_Layout.cshtml");
                template.AddEntry("materialDesign/Views/Shared/_Layout.cshtml", file1);

                /************************ FAZ AS MUDANÇAS NO CONTROLLER ****************************/

                MemoryStream aux2 = new MemoryStream();
                string file2 = "";

                // Converte a stream do controller em string pra poder fazer as modificações
                template["materialDesign/app/controllers/LeftCtrl.js"].Extract(aux2);
                aux2.Position = 0;
                sr = new StreamReader(aux2);
                file2 = sr.ReadToEnd();

                file2 = file2.Replace("//Controla o icone das opções primarias do menu\r\n", ctrl);

                // Exclui a entrada anterior e adiciona a nova
                template.RemoveEntry("materialDesign/app/controllers/LeftCtrl.js");
                template.AddEntry("materialDesign/app/controllers/LeftCtrl.js", file2);
            }

            //Salva o zip com as mudanças realizadas numa stream para ser retornada
            template.Save(tmpRetorno);

            //Retorno
            HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK);

            result.Content = new ByteArrayContent(tmpRetorno.ToArray());
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "materialDesignTemplate.zip";
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");

            return result;
        }

        // Retorna o template básico de .net com angular
        [Route("api/template/angular/{nome}")]
        public HttpResponseMessage GetAngular(string nome, [FromUri] List<string> controllers, [FromUri] List<string> services){

            //Pega o arquivo zip que contem o template a ser utilizado
            ZipFile template = ZipFile.Read(System.Web.HttpContext.Current.Server.MapPath(
                "~/Arquivos/Templates/angular.zip"));
            MemoryStream aux;

            // Cria o arquivo que declara o module do angular a ser utilizado
            string fileContent = "'use strict';\r\n\r\n" +
                            "var " + nome.ToLower() + " = angular.module('" + nome.ToLower() + "', ['ui.router']);";


            // Salva o arquivo contendo o module do app no zip
            template.AddEntry("angularTemplate/app/" + nome + ".js", fileContent);

            //modifica o nome do app no config
            aux = new MemoryStream();
            template["angularTemplate/app/appConfig.js"].Extract(aux);
            aux.Position = 0;
            var sr = new StreamReader(aux);
            string file = sr.ReadToEnd();

            // Faz a mudança e salva de volta no template
            file = file.Replace("app.config",
                nome + ".config");
            template.RemoveEntry("angularTemplate/app/appConfig.js");
            template.AddEntry("angularTemplate/app/" + nome + "Config.js", file);

            // Modifica o nome do controller principal
            aux = new MemoryStream();
            template["angularTemplate/app/controllers/HomeCtrl.js"].Extract(aux);
            aux.Position = 0;
            sr = new StreamReader(aux);
            file = sr.ReadToEnd();
            file = file.Replace("app.", nome + ".");
            template.RemoveEntry("angularTemplate/app/controllers/HomeCtrl.js");
            template.AddEntry("angularTemplate/app/controllers/HomeCtrl.js", file);

            //Insere o nome do app no layout
            aux = new MemoryStream();
            template["angularTemplate/Views/Shared/_Layout.cshtml"].Extract(aux);
            aux.Position = 0;
            file = new StreamReader(aux).ReadToEnd();

            //adiciona o nome
            file = file.Replace("ng-app=\"\"", "ng-app=\"" + nome + "\"");
            template.RemoveEntry("angularTemplate/Views/Shared/_Layout.cshtml");
            template.AddEntry("angularTemplate/Views/Shared/_Layout.cshtml", file);


            // Cria a string a ser adicionada no BundleConfig pra importação dos .js referentes ao app
            string bundle = "bundles.Add(new ScriptBundle(\"~/bundles/app\")" +
                                        ".Include(" +
                                            "\"~/app/" + nome + ".js\",\r\n" +
                                            "\"~/app/" + nome + "Config.js\",\r\n" +
                                            "\"~/app/controllers/HomeCtrl.js\"";

            //Cria o arquivo pra cada controller
            foreach (string controller in controllers)
            {
                fileContent = nome + ".controller('" + controller + "Ctrl', function ($scope) {});";

                //Salva o arquivo contendo o controller no zip
                template.AddEntry("angularTemplate/app/controllers/" + controller + "Ctrl.js", fileContent);

                //Adiciona o arquivo do controller no bundleConfig
                bundle += ",\r\n" +
                            "\"~/app/controllers/" + controller + "Ctrl.js\"";
            }

            //Cria o arquivo pra cada service
            foreach (string service in services)
            {
                fileContent = nome + ".factory('" + service + "Service', function(){});";

                //Salva o arquivo contendo o service no zip
                template.AddEntry("angularTemplate/app/services/" + service + "Service.js", fileContent);

                //Adiciona o arquivo do service no bundleConfig
                bundle += ",\r\n" +
                            "\"~/app/services/" + service + ".js\"";
            }

            bundle += "));";

            // Pega o arquivo BundleConfig pra modificação
            aux = new MemoryStream();
            template["angularTemplate/App_Start/BundleConfig.cs"].Extract(aux);
            aux.Position = 0;
            sr = new StreamReader(aux);
            file = sr.ReadToEnd();

            //Faz o replace no BundleConfig
            template.RemoveEntry("angularTemplate/App_Start/BundleConfig.cs");
            template.AddEntry("angularTemplate/App_Start/BundleConfig.cs", file.Replace("//importa arquivos app", bundle));

            //Salva o zip num memorystream pra mandar de retorno
            MemoryStream tmpRetorno = new MemoryStream();
            template.Save(tmpRetorno);

            //Monta a montagem de retorno com o arquivo .zip no conteúdo
            HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK);

            result.Content = new ByteArrayContent(tmpRetorno.ToArray());
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "angularTemplate.zip";
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");

            return result;
        }

    }
}
