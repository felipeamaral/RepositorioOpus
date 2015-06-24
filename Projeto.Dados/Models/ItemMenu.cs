using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Dados.Models
{
    public class ItemMenu
    {

        public ItemMenu(string nomeItem, bool submenus)
        {
            this.nomeItem = nomeItem;
            if (submenus)
            {
                this.submenus = new List<string>();
            }
        }

        public string nomeItem { get; set; }
        public List<string> submenus { get; set; }

    }
}