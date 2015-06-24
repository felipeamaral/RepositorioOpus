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

        // Retorna o template básico de .net com angular material
        [Route("api/template/material/{cor}")]
        public HttpResponseMessage GetTemplate(string cor/*, ItemMenu[] itens*/)
        {

            //Pega o arquivo zip que contem o template a ser utilizado
            ZipFile template = ZipFile.Read(System.Web.HttpContext.Current.Server.MapPath(
                "~/Arquivos/Templates/materialDesign.zip"));

            MemoryStream aux = new MemoryStream();
            string file;

            // Verifica se uma cor foi informada
            if (cor != null)
            {
                // Converte a stream em string pra poder fazer as modificações
                template["materialDesign/Content/Site.css"].Extract(aux);
                aux.Position = 0;
                var sr = new StreamReader(aux);
                file = sr.ReadToEnd();

                file = file + "\n\n/* Altera o layout das navs*/\r\nmd-toolbar.md-default-theme{\r\nbackground-color: " + "red" + ";\r\n}";

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

            //Salva o zip com as mudanças realizadas numa stream
            aux = new MemoryStream();
            template.Save(aux);

            /*FileStream arquivo = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("~/Arquivos/Templates/materialDesign.zip"));
            MemoryStream file = new MemoryStream();
            arquivo.CopyTo(file);*/

            //Retorno
            HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK);

            result.Content = new ByteArrayContent(aux.ToArray());
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "materialDesignTemplate.zip";
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");

            return result;
        }

    }
}
