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
using Ionic.Zip;
using System.Net.Http.Headers;

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
        private ComponenteImg[] getEndImagens(List<Componente> snippets, int qntdPages){

            List<ComponenteImg> retorno = new List<ComponenteImg>();
            string caminhoServer = System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port + "/Arquivos/Imagens/";

            foreach (Componente snippet in snippets)
            {
                string endImg = "http://" + caminhoServer + snippet.idComponente.ToString() + ".png";
                retorno.Add(new ComponenteImg(snippet.idComponente, endImg, snippet.nome, snippet.projeto, qntdPages));
            }

            return retorno.ToArray();

        }

        // Retorna a url das imagens dos snippets de (pageNumber-1)*qntd até (pageNumber-1)*qntd + qntd
        [Route("api/snippet/{qntd:int}/{pageNumber:int}")]
        public ComponenteImg[] Get(int qntd, int pageNumber)
        {
            List<Componente> snippets = this.snippetRepository.GetComponentes().OrderBy(c => c.idComponente).ToList();

            return setPages(snippets, qntd, pageNumber);
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
        [Route("api/snippet/busca/{nome}/{qntd:int}/{pageNumber:int}")]
        public ComponenteImg[] Get(string nome, int qntd, int pageNumber)
        {

            string nomeMin = nome.ToLower();

            // Busca pelos snippets que possuem a string informada no nome ou nas kws
            List<Componente> snippets = this.snippetRepository.GetComponentes()
                .Where(c => c.nome.ToLower().Contains(nomeMin) ||
                    c.Keyword.Any(k => k.kw.ToLower().Contains(nomeMin))).ToList();

            return setPages(snippets, qntd, pageNumber);
        }

        // Retorna a url das imagens de todos os snippets contidos no banco, filtrados por titulo, kw e projeto -- OK
        [Route("api/snippet/busca/{nome}/{projetos}/{qntd:int}/{pageNumber:int}")]
        public ComponenteImg[] GetByProjeto(string nome, string projetos, int qntd, int pageNumber)
        {

            string[] projs = projetos.Split(new Char [] {'0'});
            

            string nomeMin = nome.ToLower();

            // Busca pelos snippets que pertencem a um determinado projeto e possuem a string informada no nome ou nas kws
            List<Componente> snippets = this.snippetRepository.GetComponentes()
                .Where(c => projs.Any(p => Convert.ToInt32(p) == c.projeto) && (c.nome.ToLower().Contains(nomeMin)) ||
                    c.Keyword.Any(k => k.kw.ToLower().Contains(nomeMin))).ToList();

            return setPages(snippets, qntd, pageNumber);
        }

        // Retorna a url das imagens de todos os snippets contidos no banco, filtrados por projeto -- OK
        [Route("api/snippet/busca/{qntd:int}/{pageNumber:int}/{projetos}")]
        public ComponenteImg[] GetByProjeto(int qntd, int pageNumber, string projetos)
        {

            string[] projs = projetos.Split(new Char[] {'0'});

            List<Componente> snippets = this.snippetRepository.GetComponentes().Where(
                c => projs.Any(p => Convert.ToInt32(p) == c.projeto)).ToList();

            return setPages(snippets, qntd, pageNumber);
        }

        private ComponenteImg[] setPages(List<Componente> snippets, int qntd, int pageNumber)
        {
            /*Verifica se tem elementos suficientes pra essa pagina*/
            int qntdPagesCheias = snippets.Count() / qntd;
            int qntdThisPage = snippets.Count() % qntd;
            int qntdPage = qntdPagesCheias;
            if (qntdThisPage > 0)
            {
                qntdPage += 1;
            }

            /*Tem componentes pra encher a página*/
            if (qntdPagesCheias >= pageNumber)
            {
                return getEndImagens(snippets.GetRange((pageNumber - 1) * qntd, qntd), qntdPage);
            }

            /*Tem componentes pra colocar na página mas não enche-la*/
            else if (qntdPagesCheias == pageNumber - 1)
            {
                if (qntdThisPage > 0)
                {
                    return getEndImagens(snippets.GetRange((pageNumber - 1) * qntd, qntdThisPage), qntdPage);
                }
            }

            /*Não tem componentes suficientes pra chegar nessa página*/
            return new ComponenteImg[0];
        }

        // Pega todas as informações de um snippet específico -- OK
        public Componente Get(int id)
        {
            return this.snippetRepository.GetComponenteByID(id);
        }

        // Retorna os arquivos em um .zip
        [Route("api/snippet/{id}/files/download")]
        //public string[] GetFiles(int id)
        public HttpResponseMessage GetFiles(int id)
        {

            ZipFile zip = new ZipFile();

            //List<string> result = new List<string>();
            Componente snippet = this.snippetRepository.GetComponenteByID(id);

            //Verifica se existe um snippet com esse id
            if (snippet != null)
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Arquivos/Codigos/" + id.ToString());
                string path1 = Path.GetTempPath();

                // Verifica se o arquivo html existe
                if (File.Exists(path + "\\arq.html"))
                {
                    zip.AddFile(path + "\\arq.html", "/");
                    //result.Add(File.ReadAllText(path + "\\arq.html"));
                }
                else
                {
                    //result.Add(null);
                }

                // Verifica se o arquivo css existe
                if (File.Exists(path + "\\arq.css"))
                {
                    zip.AddFile(path + "\\arq.css", "/");
                    //result.Add(File.ReadAllText(path + "\\arq.css"));
                }
                else
                {
                    //result.Add(null);
                }

                // Verifica se o arquivo js existe
                if (File.Exists(path + "\\arq.js"))
                {
                    zip.AddFile(path + "\\arq.js", "/");
                    //result.Add(File.ReadAllText(path + "\\arq.js"));
                }
                else
                {
                    //result.Add(null);
                }
            }
            else
            {
                return null;
            }

            //Salva o arquivo num diretório temporário
            zip.Save(Path.GetTempPath() + snippet.nome + ".zip");

            FileStream arquivo = File.OpenRead(Path.GetTempPath() + snippet.nome + ".zip");

            MemoryStream file = new MemoryStream();
            arquivo.CopyTo(file);

            //Monta a resposta
            HttpResponseMessage result = null;
            result = Request.CreateResponse(HttpStatusCode.OK);

            result.Content = new ByteArrayContent(file.ToArray());
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = snippet.nome + ".zip";
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");

            //Fecha os arquivos
            arquivo.Close();
            file.Close();

            return result;
            //return result.ToArray();
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
