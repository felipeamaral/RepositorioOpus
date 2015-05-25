using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projeto.Dados.Models;

namespace Projeto.Dados.DAL
{
    public class ProjetoRepository : IProjetoRepository, IDisposable
    {
        private projetoDBContext context = new projetoDBContext();
        private bool disposed = false;

        public ProjetoRepository(projetoDBContext db){
            this.context = db;
        }

        //Retorna todos os projetos contidos no banco
        public Models.Projeto[] GetProjetos()
        {
            return this.context.Projeto.ToArray();
        }

        //Retorna um projeto através do seu id
        public Models.Projeto GetProjetoByID(int projetoID)
        {
            return this.context.Projeto.Find(projetoID);
        }

        //Insere um novo projeto no banco
        public void InsertProjeto(Models.Projeto proj)
        {
            this.context.Projeto.Add(proj);
            Save();
        }

        //Remove um projeto do banco
        public void DeleteProjeto(int projetoID)
        {
            Models.Projeto proj = GetProjetoByID(projetoID);
            if (proj != null)
            {
                this.context.Projeto.Remove(proj);
                Save();
            }
        }

        //Atualiza um projeto no banco
        public void UpdateProjeto(Models.Projeto proj)
        {
            this.context.Entry(proj).State = System.Data.Entity.EntityState.Modified;
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