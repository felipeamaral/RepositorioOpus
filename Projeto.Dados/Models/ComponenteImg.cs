using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Dados.Models
{
    public class ComponenteImg
    {
        public ComponenteImg(int idComponente, string endImagem, string nome)
        {
            this.endImagem = endImagem;
            this.idComponente = idComponente;
            this.nome = nome;
        }

        public string endImagem { get; set; }
        public int idComponente { get; set; }
        public string nome { get; set; }
    }
}