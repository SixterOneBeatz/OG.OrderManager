using ApplicationProtos = OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Client.Services.Greeter
{
    public class GreeterService : IGreeterService
    {
        private readonly ApplicationProtos.Greeter.GreeterClient _greeterClient;
        public GreeterService(IServiceProvider serviceProvider)
        {
            _greeterClient = serviceProvider.GetRequiredService<ApplicationProtos.Greeter.GreeterClient>();
        }
        public async Task<string> Greet(string name)
        {
            var request = new ApplicationProtos.HelloRequest { Name = name };
            var reply = await _greeterClient.SayHelloAsync(request);

            return reply.Message ?? string.Empty;
        }
    }
}
