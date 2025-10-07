using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using PB.Commons.Api.Models;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Events;

namespace PB.PropostaDeCredito.Application.EventHandler
{
    internal class CreditoDisponibilizadoEventHandler(IBus bus, IConfiguration configuration) : INotificationHandler<CreditoDisponibilizadoEvent>
    {
        private readonly IBus _bus = bus;
        private readonly IConfiguration _configuration = configuration;

        public async Task Handle(CreditoDisponibilizadoEvent notification, CancellationToken cancellationToken)
        {
            var message = new CreditoDisponibilizadoMessage(notification.ClienteId, notification.Score, notification.CreditoDisponivel);

            var endPoint = await _bus.GetSendEndpoint(new Uri($"queue:{_configuration["RabbitMQ:PropostaQueue"]}" ?? throw new InvalidOperationException("RabbitMQ:ClientesQueue configuration is missing.")));
            await endPoint.Send(message, cancellationToken);
        }
    }
}
