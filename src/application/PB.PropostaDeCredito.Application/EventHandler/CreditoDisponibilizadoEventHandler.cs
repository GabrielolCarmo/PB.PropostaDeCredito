using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using PB.Commons.Api.Models;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Events;

namespace PB.PropostaDeCredito.Application.EventHandler
{
    /// <summary>
    /// Manipulador de evento para quando um cliente tem crédito disponibilizado.
    /// Responsável por enviar a mensagem para fila RabbitMQ, realizando
    /// o mapping do evento para o tipo de mensagem compartilhada entre serviços,
    /// evitando assim ter que acoplar os dois projetos.
    /// </summary>
    public class CreditoDisponibilizadoEventHandler(IBus bus, IConfiguration configuration) : INotificationHandler<CreditoDisponibilizadoEvent>
    {
        private readonly IBus _bus = bus;
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Manipula o evento de crédito disponibilizado, enviando mensagem para a fila, já mapeada para o tipo de mensagem correspondente,
        /// utilizamos o GetSendEndpoint pois assim garantimos que mesmo que a fila não exista, ela será criada automaticamente.
        /// </summary>
        /// <param name="notification">Evento de crédito disponibilizado.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        public async Task Handle(CreditoDisponibilizadoEvent notification, CancellationToken cancellationToken)
        {
            var message = new CreditoDisponibilizadoMessage(notification.ClienteId, notification.Score, notification.CreditoDisponivel);

            var endPoint = await _bus.GetSendEndpoint(new Uri($"queue:{_configuration["RabbitMQ:PropostaQueue"]}" ?? throw new InvalidOperationException("RabbitMQ:ClientesQueue configuration is missing.")));
            await endPoint.Send(message, cancellationToken);
        }
    }
}
