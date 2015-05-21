using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Projeto.Dados.Models;
using Projeto.Dados.DAL;

namespace Projeto.Dados.Controllers
{
    public class SnippetController : ApiController
    {

        private projetoDBContext db = new projetoDBContext();
        private IComponenteRepository snippetRepository;

        public SnippetController()
        {
            this.snippetRepository = new ComponenteRepository(this.db);
        }

        // Retorna a url das imagens de todos os snippets contidos no banco
        public String[] Get()
        {

            List<Componente> snippets = this.snippetRepository.GetComponentes().ToList();

            string caminhoServer = System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port + "/Arquivos/Imagens/";
            List<string> retorno = new List<string>();

            foreach (Componente snippet in snippets)
            {
                retorno.Add(caminhoServer + snippet.idComponente.ToString() + ".png");
            }

            return retorno.ToArray();
        }

    }
}
