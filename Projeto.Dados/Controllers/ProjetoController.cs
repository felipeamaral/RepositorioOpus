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
    public class ProjetoController : ApiController
    {
        private projetoDBContext db = new projetoDBContext();
        private IProjetoRepository projetoRepository;

        // Construtor
        public ProjetoController()
        {
            this.projetoRepository = new ProjetoRepository(this.db);
        }

        // api/projeto
        // Retorna todos os projetos contidos no banco
        public Models.Projeto[] Get()
        {
            return this.projetoRepository.GetProjetos();
        }

        //api/projeto/busca/{nome}
        [Route("api/projeto/busca/{nome}")]
        // Retorna todos os projetos contidos no banco cujo nome possuem a string informada
        public Models.Projeto[] Get(string nome)
        {
            return this.projetoRepository.GetProjetos().Where(p => p.nome.ToLower().Contains(nome.ToLower())).ToArray();
        }

        //api/projeto/id
        // Retorna um projeto a partir do seu id
        public Models.Projeto Get(int id)
        {
            return this.projetoRepository.GetProjetoByID(id);
        }
    }
}
