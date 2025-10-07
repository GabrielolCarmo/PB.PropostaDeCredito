using MassTransit;
using PB.Commons.Api.Models;

namespace PB.PropostaDeCredito.Api.Consumers
{
    public class PropostaDeCreditoConsumer : IConsumer<NovoClienteCriadoMessage>
    {
        public Task Consume(ConsumeContext<NovoClienteCriadoMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}
