using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Dados.Models
{
    public class ComponenteImg
    {
        public ComponenteImg(int idComponente, string endImagem, string nome, int? idProjeto, int qntdPages)
        {
            this.endImagem = endImagem;
            this.idComponente = idComponente;
            this.nome = nome;
            this.qntdPages = qntdPages;
            this.idProjeto = idProjeto;
        }

        public string endImagem { get; set; }
        public int idComponente { get; set; }
        public string nome { get; set; }
        public int qntdPages { get; set; }
        public int? idProjeto { get; set; }
    }
}