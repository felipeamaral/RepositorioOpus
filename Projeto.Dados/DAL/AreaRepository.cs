using Projeto.Dados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Dados.DAL
{
    public class AreaRepository : IAreaRepository, IDisposable
    {

        private projetoDBContext context = new projetoDBContext();
        private bool disposed = false;

        public AreaRepository(projetoDBContext db){
            this.context = db;
        }

        public Area[] GetAreas() {
            return this.context.Area.ToArray();
        }

        public Area GetAreaByNome(string nome) {
            return this.context.Area.Find(nome);
        }

        public void InsertArea(Area area) {
            this.context.Area.Add(area);
        }

        public void DeleteArea(string nome){

            Area area = this.GetAreaByNome(nome);
            if (area != null)
            {
                this.context.Area.Remove(area);
                Save();
            }
            
        }

        public void UpdateArea(Area area)
        {
            this.context.Entry(area).State = System.Data.Entity.EntityState.Modified;
        }

        public void Save() {
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