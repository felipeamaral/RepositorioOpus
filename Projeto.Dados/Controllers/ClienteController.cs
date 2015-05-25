using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Projeto.Dados.DAL;
using Projeto.Dados.Models;

namespace Projeto.Dados.Controllers
{
    public class ClienteController : ApiController
    {
        private projetoDBContext db = new projetoDBContext();
        private IClienteRepository clienteRepository;

        // Construtor
        public ClienteController()
        {
            this.clienteRepository = new ClienteRepository(this.db);
        }

        //api/cliente
        // Retorna todos os clientes contidos no banco
        public Cliente[] Get() {
            return this.clienteRepository.GetClientes();
        }

        //api/cliente/nome
        [Route("api/cliente/{nome}")]
        // Dada um nome, retorna o cliente ao qual ele pertence
        public Cliente Get(string nome)
        {
            return this.clienteRepository.GetClienteByNome(nome);
        }

        //api/cliente/busca/nome
        [Route("api/cliente/busca/{nome}")]
        // Retorna todos os clientes que contém no seu nome a string passada (busca relativa)
        public Cliente[] GetRelative(string nome)
        {
            return this.clienteRepository.GetClientes().Where(c => c.nome.ToLower().Contains(nome.ToLower())).ToArray();
        }
    }
}
