using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Projeto.Dados.Models;
using Projeto.Dados.DAL;
using System.IO;
using System.Text;
using System.Net.Http.Headers;

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

        // Retorna a url das imagens de todos os snippets contidos no banco, filtrados por titulo e keyword
        [Route("api/snippet/busca/{nome}/{idProjeto}")]
        public String[] Get(string nome)
        {
            string nomeMin = nome.ToLower();

            // Busca pelos snippets que possuem a string informada no nome ou nas kws
            List<Componente> snippets = this.snippetRepository.GetComponentes()
                .Where(c => c.nome.ToLower().Contains(nomeMin) || 
                    c.Keyword.Any(k => k.kw.ToLower().Contains(nomeMin))).ToList();


            string caminhoServer = System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port + "/Arquivos/Imagens/";
            List<string> retorno = new List<string>();

            foreach (Componente snippet in snippets)
            {
                retorno.Add(caminhoServer + snippet.idComponente.ToString() + ".png");
            }

            return retorno.ToArray();
        }

        // Retorna a url das imagens de todos os snippets contidos no banco, filtrados por titulo, kw e projeto
        [Route("api/snippet/busca/{nome}/{idProjeto}")]
        public String[] GetByProjeto(string nome, int idProjeto)
        {

            string nomeMin = nome.ToLower();

            // Busca pelos snippets que pertencem a um determinado projeto e possuem a string informada no nome ou nas kws
            List<Componente> snippets = this.snippetRepository.GetComponentes()
                .Where(c => c.projeto == idProjeto && (c.nome.ToLower().Contains(nomeMin)) ||
                    c.Keyword.Any(k => k.kw.ToLower().Contains(nomeMin))).ToList();

            
            string caminhoServer = System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port + "/Arquivos/Imagens/";
            List<string> retorno = new List<string>();

            foreach (Componente snippet in snippets)
            {
                retorno.Add(caminhoServer + snippet.idComponente.ToString() + ".png");
            }

            return retorno.ToArray();
        }

        // Retorna a url das imagens de todos os snippets contidos no banco, filtrados por projeto
        [Route("api/snippet/busca/{idProjeto}")]
        public String[] GetByProjeto(int idProjeto)
        {

            List<Componente> snippets = this.snippetRepository.GetComponentes().Where(c => c.projeto == idProjeto).ToList();

            string caminhoServer = System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port + "/Arquivos/Imagens/";
            List<string> retorno = new List<string>();

            foreach (Componente snippet in snippets)
            {
                retorno.Add(caminhoServer + snippet.idComponente.ToString() + ".png");
            }

            return retorno.ToArray();
        }

        // Pega todas as informações de um snippet específico
        public Componente Get(int id)
        {
            return this.snippetRepository.GetComponenteByID(id);
        }

        // Retorna os arquivos
        [Route("api/snippet/{id}/files")]
        public string[] GetFiles(int id)
        {

            List<string> result = new List<string>();

            //Verifica se existe um snippet com esse id
            if (this.snippetRepository.GetComponenteByID(id) != null)
            {

                string path = @"C:\Users\manoela.OPUS\Documents\Projeto_Final_Estagio\RepositorioOpus\Projeto.Dados\Arquivos\Codigos\" + id.ToString();

                // Verifica se o arquivo html existe
                if (File.Exists(path + "\\arq.html"))
                {
                    result.Add(File.ReadAllText(path + "\\arq.html"));
                }
                else
                {
                    result.Add(null);
                }

                // Verifica se o arquivo css existe
                if (File.Exists(path + "\\arq.css"))
                {
                    result.Add(File.ReadAllText(path + "\\arq.css"));
                }
                else
                {
                    result.Add(null);
                }

                // Verifica se o arquivo js existe
                if (File.Exists(path + "\\arq.js"))
                {
                    result.Add(File.ReadAllText(path + "\\arq.js"));
                }
                else
                {
                    result.Add(null);
                }
            }

            return result.ToArray();
        }
    }
}
