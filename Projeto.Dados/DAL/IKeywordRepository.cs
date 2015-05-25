using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Dados.Models;

namespace Projeto.Dados.DAL
{
    interface IKeywordRepository : IDisposable
    {
        Keyword[] GetKeywords();
        Keyword GetKeywordByKw(string kw);
        void InsertKeyword(Keyword kw);
        void DeleteKeyword(string kw);
        void UpdateKeyword(Keyword kw);
        void Save();
    }
}
