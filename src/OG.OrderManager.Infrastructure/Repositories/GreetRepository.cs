using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Infrastructure.Repositories
{
    public class GreetRepository : IGreetRepository
    {
        public async Task<HelloReply> SayHello(HelloRequest request)
            => await Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
    }
}
