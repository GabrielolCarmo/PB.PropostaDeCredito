using PB.Commons.Infra.Kernel.Data;
using PB.PropostaDeCredito.Infra.Data.Context;

namespace PB.PropostaDeCredito.Infra.Data
{
    public class UnityOfWork(PBPropostaDeCreditoDBContext context) : IUnityOfWork
    {
        private readonly PBPropostaDeCreditoDBContext _context = context;

        public async Task<bool> CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
