using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PB.Commons.Infra.Kernel.Data;
using PB.PropostaDeCredito.Domain.PropostasDeCredito.Services;
using PB.PropostaDeCredito.Infra.Data.Context;
using PB.PropostaDeCredito.Infra.Data.PropostasDeCredito;

namespace PB.PropostaDeCredito.Infra.Data
{
    public static class ServicesExtensions
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWork>();
            services.AddScoped<IPropostaCreditoRepository, PropostaCreditoRepository>();

            services.AddDbContext<PBPropostaDeCreditoDBContext>((provider, opt) =>
            {
                opt.UseInMemoryDatabase("memorydatabase");
            });
        }
    }
}