using MediatR;
using PB.Commons.Infra.Kernel.Application;
using PB.PropostaDeCredito.Domain.PropostasDeCredito;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Command;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Services;

namespace PB.PropostaDeCredito.Application.CommandHandlers.PropostasDeCredito
{
    /// <summary>
    /// Handler do comando para gerar uma proposta de crédito, realiza a orquestração entre os serviços de domínio.
    /// </summary>
    public class GerarPropostaDeCreditoCommandHandler(IMediator mediator, IPropostaCreditoRepository repository) : IRequestHandler<GerarPropostaDeCreditoCommand, IServiceOperationResult>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPropostaCreditoRepository _repository = repository;

        /// <summary>
        /// Orquestra o processo de criação de uma nova proposta de crédito.
        /// </summary>
        /// <param name="command">Comando de criação de proposta de crédito.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Resultado da operação de serviço.</returns>
        public async Task<IServiceOperationResult> Handle(GerarPropostaDeCreditoCommand request, CancellationToken cancellationToken)
        {
            var result = new ServiceOperationResult();
            var propostaDeCredito = PropostaCredito.Factory.Create(request);

            await _repository.PersistirPropostaAsync(propostaDeCredito, cancellationToken);
            return await result.PublishEvents(propostaDeCredito, _mediator, cancellationToken);
        }
    }
}
