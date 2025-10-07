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
