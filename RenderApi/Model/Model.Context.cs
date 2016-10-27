﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RenderApi.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class DbEntities : DbContext
    {
        public DbEntities()
            : base("name=DbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Perguntas> Perguntas { get; set; }
        public DbSet<Questionarios> Questionarios { get; set; }
        public DbSet<Respostas> Respostas { get; set; }
        public DbSet<Tipos> Tipos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Renderizar> Renderizar { get; set; }
        public DbSet<Prenchimentos> Prenchimentos { get; set; }
    
        public virtual int PROCEDURE_QUEST(Nullable<int> id_quest, Nullable<int> id_render, string user)
        {
            var id_questParameter = id_quest.HasValue ?
                new ObjectParameter("id_quest", id_quest) :
                new ObjectParameter("id_quest", typeof(int));
    
            var id_renderParameter = id_render.HasValue ?
                new ObjectParameter("id_render", id_render) :
                new ObjectParameter("id_render", typeof(int));
    
            var userParameter = user != null ?
                new ObjectParameter("user", user) :
                new ObjectParameter("user", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PROCEDURE_QUEST", id_questParameter, id_renderParameter, userParameter);
        }
    
        public virtual ObjectResult<Procedure_Result> Procedure(Nullable<int> param1, Nullable<int> param2)
        {
            var param1Parameter = param1.HasValue ?
                new ObjectParameter("param1", param1) :
                new ObjectParameter("param1", typeof(int));
    
            var param2Parameter = param2.HasValue ?
                new ObjectParameter("param2", param2) :
                new ObjectParameter("param2", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Procedure_Result>("Procedure", param1Parameter, param2Parameter);
        }
    
        public virtual ObjectResult<Nullable<int>> realiza_preenchimento(Nullable<int> id_quest, Nullable<int> id_render, string user)
        {
            var id_questParameter = id_quest.HasValue ?
                new ObjectParameter("id_quest", id_quest) :
                new ObjectParameter("id_quest", typeof(int));
    
            var id_renderParameter = id_render.HasValue ?
                new ObjectParameter("id_render", id_render) :
                new ObjectParameter("id_render", typeof(int));
    
            var userParameter = user != null ?
                new ObjectParameter("user", user) :
                new ObjectParameter("user", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("realiza_preenchimento", id_questParameter, id_renderParameter, userParameter);
        }
    }
}
