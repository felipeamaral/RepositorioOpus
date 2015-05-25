using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projeto.Dados.Models;

namespace Projeto.Dados.DAL
{
    public class ComponenteRepository : IComponenteRepository, IDisposable
    {
        private projetoDBContext context = new projetoDBContext();
        private bool disposed = false;

        public ComponenteRepository(projetoDBContext db){
            this.context = db;
        }

        //Retorna todos os componentes contidos no banco
        public Componente[] GetComponentes()
        {
            return this.context.Componente.Include("Keyword").ToArray();
        }

        //Retorna um componente através do seu id
        public Componente GetComponenteByID(int componenteID)
        {
            return this.context.Componente.Find(componenteID);
        }

        //Insere um novo dado no banco
        public void InsertComponente(Componente comp)
        {
            this.context.Componente.Add(comp);
            Save();
        }

        //Remove um componente do banco
        public void DeleteComponente(int componenteID)
        {
            Componente comp = GetComponenteByID(componenteID);
            this.context.Componente.Remove(comp);
            Save();
        }

        //Atualiza um componente no banco
        public void UpdateComponente(Componente comp)
        {
            this.context.Entry(comp).State = System.Data.Entity.EntityState.Modified;
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