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
    public class AreaController : ApiController
    {
        private projetoDBContext db = new projetoDBContext();
        private IAreaRepository areaRepository;

        // Construtor
        public AreaController()
        {
            this.areaRepository = new AreaRepository(this.db);
        }

        //api/area
        // Retorna todas as áreas contidas no banco
        public Area[] Get() {
            return this.areaRepository.GetAreas();
        }

        //api/area/{nome}
        // Retorna uma determinada área
        public Area Get(string nome)
        {
            return this.areaRepository.GetAreaByNome(nome);
        }
    }
}
