using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Options;
using OG.OrderManager.Application.Common.Protos;
using OG.OrderManager.Application.Greet.Commands;

namespace OG.OrderManager.Grpc.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly IMediator _mediator;
        private readonly GlobalSettings _settings;

        public GreeterService(IMediator mediator, IOptions<GlobalSettings> options)
        {
            _mediator = mediator;
            _settings = options.Value;
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
            => await _mediator.Send(new SayHelloCommand(request));
    }
}