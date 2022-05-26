using Grpc.Core;
using MediatR;
using OG.OrderManager.Application.Common.Protos;
using OG.OrderManager.Application.Greet.Commands;

namespace OG.OrderManager.Grpc.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IMediator _mediator;
        public GreeterService(ILogger<GreeterService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
            => await _mediator.Send(new SayHelloCommand(request));
    }
}