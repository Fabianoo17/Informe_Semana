using System.Data.Entity.ModelConfiguration;

namespace Core.Logging.Data
{
    public class AuditoriaMapping : EntityTypeConfiguration<AuditoriaModel>
    {
        public AuditoriaMapping()
        {
            ToTable("T001_LOG_AUDITORIA");
            HasKey(x => x.LogId);      
        }
    }
}