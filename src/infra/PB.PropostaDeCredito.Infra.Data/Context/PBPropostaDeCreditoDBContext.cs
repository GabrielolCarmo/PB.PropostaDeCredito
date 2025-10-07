using Microsoft.EntityFrameworkCore;
using PB.PropostaDeCredito.Infra.Data.Context.Mapping;

namespace PB.PropostaDeCredito.Infra.Data.Context
{
    public class PBPropostaDeCreditoDBContext(DbContextOptions<PBPropostaDeCreditoDBContext> opt) : DbContext(opt)
    {
        public readonly DbContextOptions<PBPropostaDeCreditoDBContext> _opt = opt;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PropostaCreditoMap());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
