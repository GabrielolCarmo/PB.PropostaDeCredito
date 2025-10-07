using PB.Commons.Infra.Kernel.Domain;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Command;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Events;

namespace PB.PropostaDeCredito.Domain.PropostasDeCredito
{
    /// <summary>
    /// Representa uma proposta de crédito, agregando regras de negócio para análise e liberação de crédito.
    /// A motivação desta classe é centralizar a lógica de decisão e persistência de propostas, garantindo integridade e coesão.
    /// </summary>
    public class PropostaCredito : AggregateRoot
    {
        private PropostaCredito(Guid id, Guid clienteId, int score) : base(id)
        {
            ClienteId = clienteId;
            Score = score;
        }

        public Guid ClienteId { get; private set; }

        public int Score { get; private set; }

        public decimal CreditoDisponivel { get; private set; }

        /// <summary>
        /// Realiza a análise de crédito, definindo o valor disponível e publicando evento caso aprovado.
        /// </summary>
        public void RealizaAnalizaDeCredito()
        {
            if (!ScorePermiteLiberacaoDeCredito())
            {
                return;
            }

            DefineCreditoDisponivel();
            PublishEvent(new CreditoDisponibilizadoEvent(this));
        }

        /// <summary>
        /// Define o valor de crédito disponível conforme o score do cliente.
        /// </summary>
        private void DefineCreditoDisponivel()
        {
            if (Score < 501)
            {
                CreditoDisponivel = 1000;
                return;
            }

            CreditoDisponivel = 5000;
        }

        /// <summary>
        /// Verifica se o score do cliente permite a liberação de crédito.
        /// </summary>
        private bool ScorePermiteLiberacaoDeCredito()
        {
            return Score > 100;
        }

        /// <summary>
        /// Classe Factory para criação controlada de propostas de crédito.
        /// Garantindo que toda proposta seja criada já analisada.
        /// </summary>
        public static class Factory
        {
            /// <summary>
            /// Cria uma nova proposta de crédito a partir do comando recebido, já realizando a análise.
            /// </summary>
            /// <param name="command">Comando com dados para criação da proposta.</param>
            /// <returns>Uma nova proposta de crédito.</returns>
            public static PropostaCredito Create(GerarPropostaDeCreditoCommand command)
            {
                var proposta = new PropostaCredito(Guid.NewGuid(), command.ClientId, command.Score);

                proposta.RealizaAnalizaDeCredito();
                return proposta;
            }
        }
    }
}
