using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Dados.Models;

namespace Projeto.Dados.DAL
{
    interface IAreaRepository : IDisposable
    {
        Area[] GetAreas();
        Area GetAreaByNome(string nome);
        void InsertArea(Area area);
        void DeleteArea(string nome);
        void UpdateArea(Area area);
        void Save();
    }
}
