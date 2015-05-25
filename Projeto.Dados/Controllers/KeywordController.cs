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
    public class KeywordController : ApiController
    {
        private projetoDBContext db = new projetoDBContext();
        private IKeywordRepository kwRepository;

        // Construtor
        public KeywordController()
        {
            this.kwRepository = new KeywordRepository(this.db);
        }

        //api/keyword
        // Retorna todas as keywords contidas no banco
        public Keyword[] Get() {
            return this.kwRepository.GetKeywords();
        }

        //api/keyword/kw
        [Route("api/keyword/{kw}")]
        // Dada uma kw, retorna o objeto referente a ela, caso a mesma exista no banco
        public Keyword Get(string kw)
        {
            return this.kwRepository.GetKeywordByKw(kw);
        }

        //api/keyword/busca/kw
        [Route("api/keyword/busca/{kw}")]
        // Retorna todas as keywords que contém no seu nome a string passada (busca relativa)
        public Keyword[] GetRelative(string kw)
        {
            return this.kwRepository.GetKeywords().Where(k => k.kw.ToLower().Contains(kw.ToLower())).ToArray();
        }
    }
}
