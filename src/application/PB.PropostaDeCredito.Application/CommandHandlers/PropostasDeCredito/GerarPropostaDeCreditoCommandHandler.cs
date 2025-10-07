using MediatR;
using PB.Commons.Infra.Kernel.Application;
using PB.PropostaDeCredito.Domain.PropostasDeCredito;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Command;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Services;

namespace PB.PropostaDeCredito.Application.CommandHandlers.PropostasDeCredito
{
    public class GerarPropostaDeCreditoCommandHandler(IMediator mediator, IPropostaCreditoRepository repository) : IRequestHandler<GerarPropostaDeCreditoCommand, IServiceOperationResult>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPropostaCreditoRepository _repository = repository;

        public async Task<IServiceOperationResult> Handle(GerarPropostaDeCreditoCommand request, CancellationToken cancellationToken)
        {
            var result = new ServiceOperationResult();
            var propostaDeCredito = PropostaCredito.Factory.Create(request);

            await _repository.PersistirPropostaAsync(propostaDeCredito, cancellationToken);
            return await result.PublishEvents(propostaDeCredito, _mediator, cancellationToken);
        }
    }
}
