
using MediatR;
using PB.Commons.Infra.Kernel.Application;

namespace PB.PropostaDeCredito.Domain.PropostasDeCredito.Command
{
    public class GerarPropostaDeCreditoCommand : IRequest<IServiceOperationResult>
    {
        public Guid ClientId { get; set; }

        public int Score { get; set; }
    }
}
