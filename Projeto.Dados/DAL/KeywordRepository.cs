using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projeto.Dados.Models;

namespace Projeto.Dados.DAL
{
    public class KeywordRepository : IKeywordRepository, IDisposable
    {
        private projetoDBContext context = new projetoDBContext();
        private bool disposed = false;

        public KeywordRepository(projetoDBContext db){
            this.context = db;
        }

        //Retorna todas as keywords contidas no banco
        public Keyword[] GetKeywords()
        {
            return this.context.Keyword.ToArray();
        }

        //Retorna uma keyword através de seu nome
        public Keyword GetKeywordByKw(string kw)
        {
            return this.context.Keyword.Find(kw);
        }

        //Insere uma nova keyword no banco
        public void InsertKeyword(Keyword kw)
        {
            this.context.Keyword.Add(kw);
            Save();
        }

        //Remove uma keyword do banco
        public void DeleteKeyword(string kw)
        {
            Keyword keyword = GetKeywordByKw(kw);
            if (keyword != null)
            {
                this.context.Keyword.Remove(keyword);
                Save();
            }
        }

        //Atualiza uma keyword no banco
        public void UpdateKeyword(Keyword kw)
        {
            this.context.Entry(kw).State = System.Data.Entity.EntityState.Modified;
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