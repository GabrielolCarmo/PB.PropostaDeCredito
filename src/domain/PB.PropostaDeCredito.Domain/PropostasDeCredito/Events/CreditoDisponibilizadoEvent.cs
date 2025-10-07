using PB.Commons.Infra.Kernel.Domain;

namespace PB.PropostaDeCredito.Domain.PropostasDeCredito.Events
{
    public class CreditoDisponibilizadoEvent(PropostaCredito proposta) : IDomainEvent
    {
        public Guid AggregateRootId { get; } = proposta.Id;

        public string EventType => "CREDITO_DISPONIBILIZADO";

        public Guid ClienteId { get; } = proposta.ClienteId;

        public int Score { get; } = proposta.Score;

        public decimal CreditoDisponivel { get; } = proposta.CreditoDisponivel;
    }
}
