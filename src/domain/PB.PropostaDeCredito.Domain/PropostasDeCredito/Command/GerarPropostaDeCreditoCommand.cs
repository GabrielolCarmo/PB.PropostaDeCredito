
using MediatR;
using PB.Commons.Infra.Kernel.Application;

namespace PB.PropostaDeCredito.Domain.PropostasDeCredito.Command
{
    /// <summary>
    /// Comando para solicitar a geração de uma nova proposta de crédito.
    /// Encapsular os dados necessários para iniciar o processo de proposta.
    /// </summary>
    public class GerarPropostaDeCreditoCommand : IRequest<IServiceOperationResult>
    {
        public Guid ClientId { get; set; }

        public int Score { get; set; }
    }
}
