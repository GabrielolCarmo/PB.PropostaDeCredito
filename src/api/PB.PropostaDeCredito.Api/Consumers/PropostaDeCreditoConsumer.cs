using MassTransit;
using PB.Commons.Api.Models;
using PB.Commons.Infra.Kernel.Data;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Command;

namespace PB.PropostaDeCredito.Api.Consumers
{
    public class PropostaDeCreditoConsumer(MediatR.IMediator mediator, IUnityOfWork uow) : IConsumer<NovoClienteCriadoMessage>
    {
        private readonly MediatR.IMediator _mediator = mediator;
        private readonly IUnityOfWork _uow = uow;

        /// <summary>
        /// Consome a mensagem de novo cliente criado, mapeando para o comando que gera uma nova proposta de crédito.
        /// </summary>
        /// <param name="context">Contexto da mensagem recebida.</param>
        public async Task Consume(ConsumeContext<NovoClienteCriadoMessage> context)
        {
            var message = context.Message;
            var command = new GerarPropostaDeCreditoCommand
            {
                ClientId = message.ClienteId,
                Score = message.Score,
            };

            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                await _uow.CommitTransactionAsync();
            }
        }
    }
}
