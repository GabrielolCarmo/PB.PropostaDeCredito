using PB.Commons.Infra.Kernel.Domain;

namespace PB.PropostaDeCredito.Domain.PropostasDeCredito.Events
{
    public class CreditoDisponibilizadoEvent(PropostaCredito proposta) : IDomainEvent
    {
        public Guid AggregateRootId { get; } = proposta.Id;

        public string EventType => "CREDITO_DISPONIBILIZADO";
    }
}
