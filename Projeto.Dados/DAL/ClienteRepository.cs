using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projeto.Dados.Models;
using Projeto.Dados.DAL;

namespace Projeto.Dados.DAL
{
    public class ClienteRepository : IClienteRepository, IDisposable
    {
        private projetoDBContext context = new projetoDBContext();
        private bool disposed = false;

        public ClienteRepository(projetoDBContext db){
            this.context = db;
        }

        //Retorna todos os clientes contidos no banco
        public Cliente[] GetClientes()
        {
            return this.context.Cliente.ToArray();
        }

        //Retorna um cliente através de seu nome
        public Cliente GetClienteByNome(string nome)
        {
            return this.context.Cliente.Find(nome);
        }

        //Insere um novo cliente no banco
        public void InsertCliente(Cliente cliente)
        {
            this.context.Cliente.Add(cliente);
            Save();
        }

        //Remove um cliente do banco
        public void DeleteCliente(string nome)
        {
            Cliente cliente = GetClienteByNome(nome);
            if (cliente != null)
            {
                this.context.Cliente.Remove(cliente);
                Save();
            }
        }

        //Atualiza um cliente no banco
        public void UpdateCliente(Cliente cliente)
        {
            this.context.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
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