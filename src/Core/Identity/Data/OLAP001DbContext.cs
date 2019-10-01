using Core.Identity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Core.Identity.Data
{
    public class OLAP001DbContext : DbContext
    {
        public OLAP001DbContext()
            : base("OLAP001ConnectionString")
        {
            Database.SetInitializer<OLAP001DbContext>(null);
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 360;
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}