using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Dados.Models;

namespace Projeto.Dados.DAL
{
    interface IUsuarioRepository : IDisposable
    {
        Usuario[] GetUsuarios();
        Usuario GetUsuarioByEmail(string email);
        void InsertUsuario(Usuario user);
        void DeleteUsuario(string email);
        void UpdateUsuario(Usuario user);
        void Save();
    }
}
