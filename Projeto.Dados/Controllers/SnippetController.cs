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

        // Monta os endereços de um conjunto de imagens
        private String[] getEndImagens(List<Componente> snippets){

            List<string> retorno = new List<string>();
            string caminhoServer = System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port + "/Arquivos/Imagens/";

            foreach (Componente snippet in snippets)
            {
                retorno.Add(caminhoServer + snippet.idComponente.ToString() + ".png");
            }

            return retorno.ToArray();

        }


        // Retorna a url das imagens de todos os snippets contidos no banco -- OK
        public String[] Get()
        {
            List<Componente> snippets = this.snippetRepository.GetComponentes().ToList();

            return getEndImagens(snippets);
        }

        // Retorna a url das imagens de todos os snippets contidos no banco, filtrados por titulo e keyword -- OK
        [Route("api/snippet/busca/{nome}")]
        public String[] Get(string nome)
        {
            string nomeMin = nome.ToLower();

            // Busca pelos snippets que possuem a string informada no nome ou nas kws
            List<Componente> snippets = this.snippetRepository.GetComponentes()
                .Where(c => c.nome.ToLower().Contains(nomeMin) || 
                    c.Keyword.Any(k => k.kw.ToLower().Contains(nomeMin))).ToList();

            return getEndImagens(snippets);
        }

        // Retorna a url das imagens de todos os snippets contidos no banco, filtrados por titulo, kw e projeto -- OK
        [Route("api/snippet/busca/{nome}/{idProjeto}")]
        public String[] GetByProjeto(string nome, int idProjeto)
        {

            string nomeMin = nome.ToLower();

            // Busca pelos snippets que pertencem a um determinado projeto e possuem a string informada no nome ou nas kws
            List<Componente> snippets = this.snippetRepository.GetComponentes()
                .Where(c => c.projeto == idProjeto && (c.nome.ToLower().Contains(nomeMin)) ||
                    c.Keyword.Any(k => k.kw.ToLower().Contains(nomeMin))).ToList();

            return getEndImagens(snippets);
        }

        // Retorna a url das imagens de todos os snippets contidos no banco, filtrados por projeto -- OK
        [Route("api/snippet/busca/{idProjeto:int}")]
        public String[] GetByProjeto(int idProjeto)
        {

            List<Componente> snippets = this.snippetRepository.GetComponentes().Where(c => c.projeto == idProjeto).ToList();

            return getEndImagens(snippets);
        }

        // Pega todas as informações de um snippet específico -- OK
        public Componente Get(int id)
        {
            return this.snippetRepository.GetComponenteByID(id);
        }
    }
}
