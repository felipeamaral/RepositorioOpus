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
                aux = str.Substring(espaco + 1);
                espaco = aux.IndexOf(' ');
            }
            firstUpper += char.ToUpper(aux[0]) + aux.Substring(1);

            return firstUpper;
        }

        // Retorna o template básico de .net com angular material
        [Route("api/template/material/{cor}/{rodape}")]
        public HttpResponseMessage GetTemplate(string cor, bool rodape, [FromUri] List<string> itens)
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

                // Converte a string em stream pra poder salvar no arquivo zip
                aux = new MemoryStream();
                StreamWriter writer = new StreamWriter(aux);
                writer.Write(file);
                writer.Flush();
                aux.Position = 0;

                // Exclui a entrada anterior e adiciona a nova
                template.RemoveEntry("materialDesign/Content/Site.css");
                template.AddEntry("materialDesign/Content/Site.css", aux);
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

                    itensMenu = itensMenu + "<md-list-item ng-click=\"toggle" + firstUpper + "()\">\r\n" +
                            "<p> " + char.ToUpper(item[0]) + item.Substring(1) + " </p>\r\n" +
                            "<ng-md-icon icon=\"{{icon" + firstUpper + "}}\" style=\"fill: black\" class=\"md-icon-button\"></ng-md-icon>\r\n" +
                            "</md-list-item>\r\n";

                    if (item.Equals(itens[0])){
                        itensMenu = itensMenu + "<div ng-show=\"" + firstLower + "Options\" class=\"menu-secundario\">\r\n" +
                                        "<md-list-item ng-click=\"nothing()\">\r\n" +
                                        "<p> Menu secundario </p>\r\n" +
                                        "</md-list-item>\r\n" +
                                        "</div>\r\n";
                        options1 = "$scope." + firstLower + "Options = false;\r\n";
                        options2 = "$scope." + firstLower + "Options = $scope." + firstLower + "Options == true ? false : true;\r\n";
                    }

                    // Seta o que será colocado no controller
                    ctrl = ctrl + "\r\n// Funções relacionadas ao item '" + firstUpper + "' do menu\r\n";
                    ctrl = ctrl + "$scope.icon" + firstUpper +" = \"add\";\r\n" + 
                         "$scope.toggle" + firstUpper +" = function (esconde) {\r\n" +
                                "if (esconde) {\r\n" +
                                    "$scope.icon" + firstUpper +" = \"add\";\r\n" + options1 +
                                "} else {\r\n" +
                             "$scope.icon" + firstUpper +" = $scope.icon" + firstUpper + " == \"add\" ? \"remove\" : \"add\";\r\n" +
                             options2;

                    foreach (string it in itens){
                        if (!it.Equals(item)){
                            ctrl = ctrl + "$scope.toggle" + firstLettersUpper(it) + "(true);\r\n";
                        }
                    }
                    ctrl = ctrl + "}\r\n};\r\n"; 
                }

                itensMenu = itensMenu + "</md-list>\r\n";


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

                // Converte a string em stream pra poder salvar no arquivo zip
                aux1 = new MemoryStream();
                StreamWriter writer = new StreamWriter(aux1);
                writer.Write(file1);
                writer.Flush();
                aux1.Position = 0;

                // Exclui a entrada anterior e adiciona a nova
                template.RemoveEntry("materialDesign/Views/Shared/_Layout.cshtml");
                template.AddEntry("materialDesign/Views/Shared/_Layout.cshtml", aux1);

                /************************ FAZ AS MUDANÇAS NO CONTROLLER ****************************/

                MemoryStream aux2 = new MemoryStream();
                string file2 = "";

                // Converte a stream do controller em string pra poder fazer as modificações
                template["materialDesign/app/controllers/LeftCtrl.js"].Extract(aux2);
                aux2.Position = 0;
                sr = new StreamReader(aux2);
                file2 = sr.ReadToEnd();

                file2 = file2.Replace("//Controla o icone das opções primarias do menu\r\n", ctrl);

                // Converte a string em stream pra poder salvar no arquivo zip
                aux2 = new MemoryStream();
                writer = new StreamWriter(aux2);
                writer.Write(file2);
                writer.Flush();
                aux2.Position = 0;

                // Exclui a entrada anterior e adiciona a nova
                template.RemoveEntry("materialDesign/app/controllers/LeftCtrl.js");
                template.AddEntry("materialDesign/app/controllers/LeftCtrl.js", aux2);
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

    }
}
