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
        private IClienteRepository clienteRepository;
        private IAreaRepository areaRepository;

        // Construtor
        public ProjetoController()
        {
            this.projetoRepository = new ProjetoRepository(this.db);
            this.clienteRepository = new ClienteRepository(this.db);
            this.areaRepository = new AreaRepository(this.db);
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

        // api/projeto
        //POST
        public HttpResponseMessage Post([FromBody] Models.Projeto projeto)
        {

            // Verifica se existe o cliente
            if (projeto.cliente != null)
            {
                Cliente cliente = this.clienteRepository.GetClienteByNome(projeto.cliente);
                if (cliente == null)
                {
                    cliente = new Cliente();
                    cliente.nome = projeto.cliente;
                }
                projeto.Cliente1 = cliente;
            }

            //Verifica se existe a área
            if (projeto.area != null)
            {
                Area area = this.areaRepository.GetAreaByNome(projeto.area);
                if (area == null)
                {
                    area = new Area();
                    area.nome = projeto.area;
                }
                projeto.Area1 = area;
            }

            //Salva no banco
            this.projetoRepository.InsertProjeto(projeto);

            var response = Request.CreateResponse<Models.Projeto>(System.Net.HttpStatusCode.Created, projeto);

            return response;
        }
    }
}
