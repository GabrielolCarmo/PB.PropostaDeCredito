using PB.Commons.Infra.Kernel.Domain;

namespace PB.PropostaDeCredito.Domain.PropostasDeCredito.Events
{
    /// <summary>
    /// Evento de domínio disparado quando o crédito é disponibilizado para uma proposta.
    /// </summary>
    public class CreditoDisponibilizadoEvent(PropostaCredito proposta) : IDomainEvent
    {
        public Guid AggregateRootId { get; } = proposta.Id;

        public string EventType => "CREDITO_DISPONIBILIZADO";

        public Guid ClienteId { get; } = proposta.ClienteId;

        public int Score { get; } = proposta.Score;

        public decimal CreditoDisponivel { get; } = proposta.CreditoDisponivel;
    }
}
