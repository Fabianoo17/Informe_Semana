using Core.ViewModels.Exemplo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Core.Data.Context
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext()
            : base("DefaultConnectionString")
        {
            Database.SetInitializer<DefaultDbContext>(null);

            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 360;

            //Configuration.ProxyCreationEnabled = false;
            //Configuration.LazyLoadingEnabled = false;
        }

        // Tables
        public DbSet<ExemploEntity> ExemploEntity { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(50));

            //modelBuilder.Configurations.Add(new LogEmailProjetoConfig());

            // Ignore
            //modelBuilder.Ignore<ValidationResult>();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("TotalItens") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("TotalItens").CurrentValue = false;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("TotalItens").IsModified = false;
                }
            }
            return base.SaveChanges();
        }
    }
}