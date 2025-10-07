
namespace PB.PropostaDeCredito.Domain.PropostasDeCredito.Services
{
    /// <summary>
    /// Interface para repositório de propostas de crédito, as interfaces de repositório ficam na camada de dominio pois nada mais são do que serviços de dominio.
    /// </summary>
    public interface IPropostaCreditoRepository
    {
        /// <summary>
        /// Persiste uma proposta de crédito no repositório.
        /// </summary>
        /// <param name="propostaCredito">Proposta a ser persistida.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        public Task PersistirPropostaAsync(PropostaCredito propostaCredito, CancellationToken cancellationToken);
    }
}
