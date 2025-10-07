using Microsoft.EntityFrameworkCore;
using PB.PropostaDeCredito.Domain.PropostasDeCredito;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Services;
using PB.PropostaDeCredito.Infra.Data.Context;

namespace PB.PropostaDeCredito.Infra.Data.PropostasDeCredito
{
    public class PropostaCreditoRepository(PBPropostaDeCreditoDBContext context) : IPropostaCreditoRepository
    {
        private readonly DbSet<PropostaCredito> _dbSet = context.Set<PropostaCredito>();

        public async Task PersistirPropostaAsync(PropostaCredito propostaCredito, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(propostaCredito, cancellationToken: cancellationToken);
        }
    }
}
