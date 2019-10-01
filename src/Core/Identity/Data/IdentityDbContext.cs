using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Core.Identity.Data
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext()
            : base("IdentityConnectionString")
        {
            Database.SetInitializer<IdentityDbContext>(null);
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 360;
        }
    }
}