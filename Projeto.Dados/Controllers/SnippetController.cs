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
        private IProjetoRepository projRepository;
        private IKeywordRepository kwRepository;
        private IUsuarioRepository userRepository;

        public SnippetController()
        {
            this.snippetRepository = new ComponenteRepository(this.db);
            this.projRepository = new ProjetoRepository(this.db);
            this.kwRepository = new KeywordRepository(this.db);
            this.userRepository = new UsuarioRepository(this.db);
        }

        // Monta os endereços de um conjunto de imagens
        private ComponenteImg[] getEndImagens(List<Componente> snippets){

            List<ComponenteImg> retorno = new List<ComponenteImg>();
            string caminhoServer = System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port + "/Arquivos/Imagens/";

            foreach (Componente snippet in snippets)
            {
                string endImg = "http://" + caminhoServer + snippet.idComponente.ToString() + ".png";
                retorno.Add(new ComponenteImg(snippet.idComponente, endImg, snippet.nome));
            }

            return retorno.ToArray();

        }

        //Retorna quantidade de snippets contidos no banco
        [Route("api/snippet")]
        public List<int> Get()
        {
            List<int> retorno = new List<int>();
            retorno.Add(this.snippetRepository.GetComponentes().Count());

            return retorno;
        }

        // Retorna a url das imagens dos snippets de (pageNumber-1)*qntd até (pageNumber-1)*qntd + qntd
        [Route("api/snippet/{qntd:int}/{pageNumber:int}")]
        public ComponenteImg[] Get(int qntd, int pageNumber)
        {
            List<Componente> snippets = this.snippetRepository.GetComponentes().OrderBy(c => c.idComponente).ToList();

            /*Verifica se tem elementos suficientes pra essa pagina*/
            int qntdPagesCheias = snippets.Count() / qntd;

            /*Tem componentes pra encher uma página*/
            if (qntdPagesCheias >= pageNumber)
            {
                return getEndImagens(snippets.GetRange((pageNumber - 1) * qntd, qntd));
            }
            /*Tem componentes pra colocar na página*/
            else if (qntdPagesCheias == pageNumber - 1)
            {
                int qntdPage = snippets.Count() % qntd;
                if (qntdPage > 0)
                {
                    return getEndImagens(snippets.GetRange((pageNumber - 1) * qntd, qntdPage));
                }
            }
 
            /*Não tem componentes suficientes pra chegar nessa página*/
            return new ComponenteImg[0];
        }

        // Adiciona um novo snippet -- OK
        // POST api/snippet 
        public HttpResponseMessage Post([FromBody] Componente snippet)
        {

            List<Keyword> kws = new List<Keyword>();            

            // Pega o objeto do projeto
            if (snippet.projeto != null)
            {
                snippet.Projeto1 = this.projRepository.GetProjetoByID(Convert.ToInt32(snippet.projeto));
            }

            // Verifica se as keywords existem
            foreach (Keyword kw in snippet.Keyword)
            {
                Keyword aux = this.kwRepository.GetKeywordByKw(kw.kw);
                if (aux == null)
                {
                    aux = new Keyword();
                    aux.kw = kw.kw;
                }              
                kws.Add(aux);
            }
            //Atualiza a lista de keywords do snippet
            snippet.Keyword = kws;

            //Seta o objeto do usuário
            snippet.Usuario1 = this.userRepository.GetUsuarioByEmail(snippet.usuario);

            // Insere o snippet no banco
            this.snippetRepository.InsertComponente(snippet);

            var response = Request.CreateResponse<Componente>(System.Net.HttpStatusCode.Created, snippet);

            return response;

        }

        // Edita um snippet já existente
        // PUT api/snippet/id
        public HttpResponseMessage Put(int id, [FromBody]Componente snippet)
        {
            List<Keyword> kws = new List<Keyword>();  

            // Pega o snippet a ser modificado no banco
            Componente snippetInserir = this.snippetRepository.GetComponenteByID(id);

            // Seta itens que são do próprio snippet
            snippetInserir.nome = snippet.nome;

            // Seta o usuário
            snippetInserir.Usuario1 = this.userRepository.GetUsuarioByEmail(snippet.usuario);
            snippetInserir.usuario = snippetInserir.Usuario1.email;

            // Seta o projeto
            if (snippet.projeto != null){
                snippetInserir.Projeto1 = this.projRepository.GetProjetoByID(Convert.ToInt32(snippet.projeto));
                snippetInserir.projeto = snippetInserir.Projeto1.idProjeto;
            }

            // Seta as keywords
            foreach (Keyword kw in snippet.Keyword)
            {
                Keyword aux = this.kwRepository.GetKeywordByKw(kw.kw);
                if (aux == null)
                {
                    aux = new Keyword();
                    aux.kw = kw.kw;
                }
                kws.Add(aux);
            }
            snippetInserir.Keyword.Clear();
            snippetInserir.Keyword = kws;

            // Atualiza o objeto no banco
            this.snippetRepository.UpdateComponente(snippetInserir);

            var response = Request.CreateResponse<Componente>(System.Net.HttpStatusCode.Created, snippetInserir);

            return response;

        }

        // Retorna a url das imagens de todos os snippets contidos no banco, filtrados por titulo e keyword -- OK
        [Route("api/snippet/busca/{nome}")]
        public ComponenteImg[] Get(string nome)
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
        public ComponenteImg[] GetByProjeto(string nome, int idProjeto)
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
        public ComponenteImg[] GetByProjeto(int idProjeto)
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
        public string[] GetFiles(int id)
        {

            List<string> result = new List<string>();

            //Verifica se existe um snippet com esse id
            if (this.snippetRepository.GetComponenteByID(id) != null)
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Arquivos/Codigos/" + id.ToString());

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
