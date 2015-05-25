using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Dados.Models;

namespace Projeto.Dados.DAL
{
    interface IClienteRepository : IDisposable
    {
        Cliente[] GetClientes();
        Cliente GetClienteByNome(string nome);
        void InsertCliente(Cliente cliente);
        void DeleteCliente(string nome);
        void UpdateCliente(Cliente cliente);
        void Save();
    }
}
