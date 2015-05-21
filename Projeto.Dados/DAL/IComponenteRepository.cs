using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Dados.Models;

namespace Projeto.Dados.DAL
{
    interface IComponenteRepository : IDisposable
    {
        Componente[] GetComponentes();
        Componente GetComponenteByID(int componenteID);
        void InsertComponente(Componente comp);
        void DeleteComponente(int componenteID);
        void UpdateComponente(Componente comp);
        void Save();
    }
}
