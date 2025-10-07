using MassTransit;
using PB.PropostaDeCredito.Api.Consumers;
using PB.PropostaDeCredito.Infra.Data;

namespace PB.PropostaDeCredito.Api
{
    public static class ServicesExtensions
    {
        public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataServices();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<PropostaDeCreditoConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["Rabbit:Host"] ?? "localhost", "/", h =>
                    {
                        h.Username(configuration["Rabbit:User"] ?? "guest");
                        h.Password(configuration["Rabbit:Pass"] ?? "guest");
                    });

                    cfg.ReceiveEndpoint(configuration["RabbitMQ:ClientesQueue"] ?? throw new InvalidOperationException("RabbitMQ:ClientesQueue configuration is missing."), e =>
                    {
                        e.ConfigureConsumer<PropostaDeCreditoConsumer>(context);

                        // caso todas as tentativas de reenvio imediato falhem, tenta reenviar a mensagem em 3 intervalos diferentes
                        // com 5, 15 e 30 minutos de espera entre cada tentativa
                        cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)));

                        // caso de falha na entrega da mensagem, tenta reenviar 5 vezes imediatamente
                        cfg.UseMessageRetry(r => r.Immediate(5));
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Domain.ServicesExtensions).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.ServicesExtensions).Assembly));
        }
    }
}
