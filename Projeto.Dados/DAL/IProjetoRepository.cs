using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Dados.Models;

namespace Projeto.Dados.DAL
{
    interface IProjetoRepository : IDisposable
    {
        Models.Projeto[] GetProjetos();
        Models.Projeto GetProjetoByID(int projetoID);
        void InsertProjeto(Models.Projeto proj);
        void DeleteProjeto(int projetoID);
        void UpdateProjeto(Models.Projeto proj);
        void Save();
    }
}
