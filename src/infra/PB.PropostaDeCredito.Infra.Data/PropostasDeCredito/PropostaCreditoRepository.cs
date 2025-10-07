using Microsoft.EntityFrameworkCore;
using PB.PropostaDeCredito.Domain.PropostasDeCredito;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Services;
using PB.PropostaDeCredito.Infra.Data.Context;

namespace PB.PropostaDeCredito.Infra.Data.PropostasDeCredito
{
    /// <summary>
    /// Repositório de acesso a dados para entidade Proposta de Crédito, implementação concreta.
    /// </summary>
    public class PropostaCreditoRepository(PBPropostaDeCreditoDBContext context) : IPropostaCreditoRepository
    {
        private readonly DbSet<PropostaCredito> _dbSet = context.Set<PropostaCredito>();

        /// <summary>
        /// Persiste uma proposta de crédito no repositório.
        /// </summary>
        /// <param name="propostaCredito">Proposta de crédito a ser persistida.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns></returns>
        public async Task PersistirPropostaAsync(PropostaCredito propostaCredito, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(propostaCredito, cancellationToken: cancellationToken);
        }
    }
}
