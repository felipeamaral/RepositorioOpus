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
    public class UsuarioController : ApiController
    {
        private projetoDBContext db = new projetoDBContext();
        private IUsuarioRepository userRepository;

        // Construtor
        public UsuarioController()
        {
            this.userRepository = new UsuarioRepository(this.db);
        }

        //api/usuario
        // Retorna todos os usuário contidos no banco
        public Usuario[] Get() {
            return this.userRepository.GetUsuarios();
        }

        //api/usuario/email
        [Route("api/usuario/{email}")]
        // Dada um email, retorna o usuário ao qual ele pertence
        public Usuario Get(string email)
        {
            return this.userRepository.GetUsuarioByEmail(email);
        }

        //api/usuario/busca/nome
        [Route("api/usuario/busca/{nome}")]
        // Retorna todos os usuários que contém no seu nome a string passada (busca relativa)
        public Usuario[] GetNome(string nome)
        {
            return this.userRepository.GetUsuarios().Where(u => u.nome.ToLower().Contains(nome.ToLower())).ToArray();
        }
    }
}
