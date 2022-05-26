using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Application.Common.Interfaces
{
    public interface IGreetRepository
    {
        Task<HelloReply> SayHello(HelloRequest request);
    }
}
