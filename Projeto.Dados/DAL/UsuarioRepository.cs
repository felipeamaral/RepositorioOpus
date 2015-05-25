using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projeto.Dados.Models;

namespace Projeto.Dados.DAL
{
    public class UsuarioRepository : IUsuarioRepository, IDisposable
    {
        private projetoDBContext context = new projetoDBContext();
        private bool disposed = false;

        public UsuarioRepository(projetoDBContext db){
            this.context = db;
        }

        //Retorna todos os usuarios contidos no banco
        public Usuario[] GetUsuarios()
        {
            return this.context.Usuario.ToArray();
        }

        //Retorna um usuário através de seu email
        public Usuario GetUsuarioByEmail(string email)
        {
            return this.context.Usuario.Find(email);
        }

        //Insere um novo usuário no banco
        public void InsertUsuario(Usuario user)
        {
            this.context.Usuario.Add(user);
            Save();
        }

        //Remove um usuário do banco
        public void DeleteUsuario(string email)
        {
            Usuario user = GetUsuarioByEmail(email);
            if (user != null)
            {
                this.context.Usuario.Remove(user);
                Save();
            }
        }

        //Atualiza um usuário no banco
        public void UpdateUsuario(Usuario user)
        {
            this.context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        //Salva as mudanças no banco
        public void Save()
        {
            this.context.SaveChanges();
        }

        //Métodos de descarte
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}