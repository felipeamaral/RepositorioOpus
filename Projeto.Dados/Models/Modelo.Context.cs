﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projeto.Dados.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class projetoDBContext : DbContext
    {
        public projetoDBContext()
            : base("name=projetoDBContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Componente> Componente { get; set; }
        public virtual DbSet<Keyword> Keyword { get; set; }
        public virtual DbSet<Projeto> Projeto { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
