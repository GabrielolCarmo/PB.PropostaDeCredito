using PB.Commons.Infra.Kernel.Domain;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Command;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Events;

namespace PB.PropostaDeCredito.Domain.PropostasDeCredito
{
    public class PropostaCredito : AggregateRoot
    {
        private PropostaCredito(Guid id, Guid clienteId, int score) : base(id)
        {
            ClienteId = clienteId;
            Score = score;
        }

        public Guid ClienteId { get; private set; }

        public int Score { get; private set; }

        public decimal CreditoDisponivel { get; set; }

        public void RealizaAnalizaDeCredito()
        {
            if (!ScorePermiteLiberacaoDeCredito())
            {
                return;
            }

            DefineCreditoDisponivel();
            PublishEvent(new CreditoDisponibilizadoEvent(this));
        }

        private void DefineCreditoDisponivel()
        {
            if (Score < 501)
            {
                CreditoDisponivel = 1000;
                return;
            }

            CreditoDisponivel = 5000;
        }

        private bool ScorePermiteLiberacaoDeCredito()
        {
            return Score > 100;
        }

        public static class Factory
        {
            public static PropostaCredito Create(GerarPropostaDeCreditoCommand command)
            {
                var proposta = new PropostaCredito(Guid.NewGuid(), command.ClientId, command.Score);

                proposta.RealizaAnalizaDeCredito();
                return proposta;
            }
        }
    }
}
