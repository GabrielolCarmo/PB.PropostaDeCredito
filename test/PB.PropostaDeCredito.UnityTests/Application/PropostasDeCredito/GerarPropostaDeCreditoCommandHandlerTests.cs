using Bogus;
using FluentAssertions;
using MediatR;
using Moq;
using PB.PropostaDeCredito.Application.CommandHandlers.PropostasDeCredito;
using PB.PropostaDeCredito.Domain.PropostasDeCredito;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Command;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Services;

namespace PB.PropostaDeCredito.UnityTests.Application.PropostasDeCredito
{
    public class GerarPropostaDeCreditoCommandHandlerTests
    {
        public GerarPropostaDeCreditoCommandHandlerTests()
        {
            _mediator = new Mock<IMediator>();

            _repositoryMock = new Mock<IPropostaCreditoRepository>();
            _handler = new GerarPropostaDeCreditoCommandHandler(_mediator.Object, _repositoryMock.Object);
        }

        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IPropostaCreditoRepository> _repositoryMock;
        private readonly GerarPropostaDeCreditoCommandHandler _handler;
        private static readonly Faker Faker = new("pt_BR");

        [Fact(DisplayName = "Dado um comando válido, deve criar uma nova proposta de crédito com sucesso")]
        public async Task Dado_um_comando_valido_deve_criar_uma_nova_proposta_de_credito_com_sucesso()
        {
            // Arrange
            var command = new GerarPropostaDeCreditoCommand()
            {
                ClientId = Guid.NewGuid(),
                Score = Faker.Random.Int(0, 1000)
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull("O resultado da operação não deve ser nulo.");
            result.IsSuccess.Should().BeTrue("A operação deve ser bem sucedida ao gerar uma nova proposta de crédito.");
            result.IsFailure.Should().BeFalse("A operação não deve ter falha ao gerar uma nova proposta de crédito.");
            result.Errors.Should().BeNullOrEmpty("Não deve conter erros na operação.");
        }

        [Fact(DisplayName = "Dado um comando válido, deve publicar o evento CreditoDisponibilizadoEvent quando houver crédito disponível")]
        public async Task Dado_um_comando_valido_deve_publicar_o_evento_CreditoDisponibilizadoEvent_quando_houver_credito_disponivel()
        {
            // Arrange
            var command = new GerarPropostaDeCreditoCommand()
            {
                ClientId = Guid.NewGuid(),
                Score = Faker.Random.Int(200, 1000) // Score maior que 100 para garantir crédito disponível
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull("O resultado da operação não deve ser nulo.");
            result.IsSuccess.Should().BeTrue("A operação deve ser bem sucedida ao gerar uma nova proposta de crédito.");
            result.IsFailure.Should().BeFalse("A operação não deve ter falha ao gerar uma nova proposta de crédito.");
            result.Errors.Should().BeNullOrEmpty("Não deve conter erros na operação.");
            _mediator.Verify(m => m.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce, "O evento CreditoDisponibilizadoEvent deve ser publicado quando houver crédito disponível.");
        }

        [Fact(DisplayName = "Dado um comando válido, deve persistir a nova proposta de crédito no repositório")]
        public async Task Dado_um_comando_valido_deve_persistir_a_nova_proposta_de_credito_no_repositorio()
        {
            // Arrange
            var command = new GerarPropostaDeCreditoCommand()
            {
                ClientId = Guid.NewGuid(),
                Score = Faker.Random.Int(0, 1000)
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull("O resultado da operação não deve ser nulo.");
            result.IsSuccess.Should().BeTrue("A operação deve ser bem sucedida ao gerar uma nova proposta de crédito.");
            result.IsFailure.Should().BeFalse("A operação não deve ter falha ao gerar uma nova proposta de crédito.");
            result.Errors.Should().BeNullOrEmpty("Não deve conter erros na operação.");
            _repositoryMock.Verify(r => r.PersistirPropostaAsync(It.IsAny<PropostaCredito>(), It.IsAny<CancellationToken>()), Times.Once, "A nova proposta de crédito deve ser persistida no repositório.");
        }
    }
}
