using Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Core.Data.Context
{
    public class InformeSemanaContext : DbContext
    {
        public InformeSemanaContext()
            : base("INFORME_SEMANAL")
        {

        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "ID")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));


        }

        public DbSet<T002_INFORME> T002_INFORMES { get; set; }
        public DbSet<T003_COORDENACOES> T003_COORDENACOES { get; set; }
        public DbSet<T001_INDICE_INFORME> T001_INDICE_INFORME { get; set; }
        public DbSet<T004_SEMANA> T004_SEMANA { get; set; }
        public DbSet<VW001_INFORME_ATUAL> VW001_INFORME_ATUAL { get; set; }



    }
}