using MediatR;
using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Application.Greet.Commands
{
    public class SayHelloCommand : IRequest<HelloReply>
    {
        public HelloRequest HelloRequest { get; set; }

        public SayHelloCommand(HelloRequest helloRequest)
            => HelloRequest = helloRequest;
    }

    public class SayHelloCommandHandler : IRequestHandler<SayHelloCommand, HelloReply>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SayHelloCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public Task<HelloReply> Handle(SayHelloCommand request, CancellationToken cancellationToken)
            => _unitOfWork.GreetRepository.SayHello(request.HelloRequest);
    }
}
