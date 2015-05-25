using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Projeto.Dados.DAL;
using Projeto.Dados.Models;
using System.IO;
using System.Text;

namespace Projeto.Dados.Controllers
{
    public class ArquivoController : ApiController
    {
        private projetoDBContext db = new projetoDBContext();
        private IComponenteRepository snippetRepository;

        public ArquivoController()
        {
            this.snippetRepository = new ComponenteRepository(this.db);
        }

        // Retorna os arquivos -- OK
        public string[] Get(int id)
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
