using Bogus;
using FluentAssertions;
using PB.PropostaDeCredito.Domain.PropostasDeCredito;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Command;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Events;

namespace PB.PropostaDeCredito.UnityTests.Domain.PropostasDeCredito
{
    public class GerarPropostaDeCreditoTest
    {
        private static readonly Faker Faker = new("pt_BR");

        [Fact(DisplayName = "Dado um comando válido, deve gerar um porposta de crédito")]
        public void Dado_um_comando_valido_deve_gerar_um_porposta_de_credito()
        {
            // Arrange
            var command = new GerarPropostaDeCreditoCommand()
            {
                ClientId = Guid.NewGuid(),
                Score = Faker.Random.Int(101, 1000)
            };

            // Act
            var proposta = PropostaCredito.Factory.Create(command);

            // Assert
            proposta.Should().NotBeNull("A proposta não foi criada.");
            proposta.Id.Should().NotBeEmpty("O Id da proposta não foi gerado.");
            proposta.ClienteId.Should().Be(command.ClientId, "O Id do cliente na proposta não corresponde ao comando.");
            proposta.Score.Should().Be(command.Score, "O score na proposta não corresponde ao comando.");
            proposta.CreditoDisponivel.Should().NotBe(0, "O crédito disponível deve ser maior que zero.");
        }

        [Fact(DisplayName = "Dado um comando com score menor ou igual a 100, não deve liberar crédito")]
        public void Dado_um_comando_com_score_menor_ou_igual_a_100_nao_deve_liberar_credito()
        {
            // Arrange
            var command = new GerarPropostaDeCreditoCommand()
            {
                ClientId = Guid.NewGuid(),
                Score = Faker.Random.Int(0, 100)
            };

            // Act
            var proposta = PropostaCredito.Factory.Create(command);

            // Assert
            proposta.Should().NotBeNull("A proposta não foi criada.");
            proposta.CreditoDisponivel.Should().Be(0, "O crédito disponível deve ser zero para score menor ou igual a 100.");
        }

        [Fact(DisplayName = "Dado um comando com score entre 101 e 500, deve liberar crédito de 1000")]
        public void Dado_um_comando_com_score_entre_101_e_500_deve_liberar_credito_de_1000()
        {
            // Arrange
            var command = new GerarPropostaDeCreditoCommand()
            {
                ClientId = Guid.NewGuid(),
                Score = Faker.Random.Int(101, 500)
            };

            // Act
            var proposta = PropostaCredito.Factory.Create(command);

            // Assert
            proposta.Should().NotBeNull("A proposta não foi criada.");
            proposta.CreditoDisponivel.Should().Be(1000, "O crédito disponível deve ser 1000 para score entre 101 e 500.");
        }

        [Fact(DisplayName = "Dado um comando com score maior que 500, deve liberar crédito de 5000")]
        public void Dado_um_comando_com_score_maior_que_500_deve_liberar_credito_de_5000()
        {
            // Arrange
            var command = new GerarPropostaDeCreditoCommand()
            {
                ClientId = Guid.NewGuid(),
                Score = Faker.Random.Int(501, 1000)
            };

            // Act
            var proposta = PropostaCredito.Factory.Create(command);

            // Assert
            proposta.Should().NotBeNull("A proposta não foi criada.");
            proposta.CreditoDisponivel.Should().Be(5000, "O crédito disponível deve ser 5000 para score maior que 500.");
        }

        [Fact(DisplayName = "Dado um comando com score maior que 100, deve publicar o evento CreditoDisponibilizadoEvent")]
        public void Dado_um_comando_com_score_maior_que_100_deve_publicar_o_evento_CreditoDisponibilizadoEvent()
        {
            // Arrange
            var command = new GerarPropostaDeCreditoCommand()
            {
                ClientId = Guid.NewGuid(),
                Score = Faker.Random.Int(101, 1000)
            };

            // Act
            var proposta = PropostaCredito.Factory.Create(command);

            // Assert
            proposta.Events.Should().NotBeNull("A lista de eventos não deve ser nula.");
            proposta.Events[0].Should().BeOfType<CreditoDisponibilizadoEvent>("O evento CreditoDisponibilizadoEvent deve ser publicado quando o crédito é liberado.");
        }
    }
}
