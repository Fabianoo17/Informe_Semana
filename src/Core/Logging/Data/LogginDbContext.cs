using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Core.Logging.Data
{
    public class LogginDbContext : DbContext
    {
        public LogginDbContext()
            : base("LoggingConnectionString")
        {
            Database.SetInitializer<LogginDbContext>(null);
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 360;
        }

        public DbSet<AuditoriaModel> LogAuditoria { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AuditoriaMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}