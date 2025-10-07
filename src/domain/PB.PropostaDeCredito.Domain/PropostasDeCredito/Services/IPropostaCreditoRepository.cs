
namespace PB.PropostaDeCredito.Domain.PropostasDeCredito.Services
{
    public interface IPropostaCreditoRepository
    {
        public Task PersistirPropostaAsync(PropostaCredito propostaCredito, CancellationToken cancellationToken);
    }
}
