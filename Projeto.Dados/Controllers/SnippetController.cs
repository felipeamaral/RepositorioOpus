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
using System.Web;

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

        // Retorna os arquivos -- OK
        [Route("api/snippet/{id}/files/download")]
        public string[] DownloadFiles(int id)
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

        /*Faz upload dos arquivos de um snippet*/
        [Route("api/snippet/{id}/files/upload")]
        public void UploadFiles(int id)
        {
            var request = HttpContext.Current.Request;
            string nomeArquivo, caminho, extensaoArquivo;
            int ponto;

            // Verifica se não foram enviados mais de 3 arquivos
            if (request.Files.Count <= 3)
            {
                // Pra cada arquivo pega a sua extensão e salva no diretório de códigos, na pasta do snippet
                for (int i = 0; i < request.Files.Count; i++)
                {
                    //Seta o nome do arquivo sem a extensão
                    nomeArquivo = request.Files[i].FileName;
                    ponto = nomeArquivo.IndexOf('.');
                    extensaoArquivo = nomeArquivo.Substring(ponto + 1);
                    nomeArquivo = nomeArquivo.Substring(0, ponto);

                    //Seta o nome do arquivo a ser salvo
                    caminho = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Arquivos/Codigos/" + id),
                        ("arq." + extensaoArquivo));
                    request.Files[i].SaveAs(caminho);
                }
            }
        }
    }
}
