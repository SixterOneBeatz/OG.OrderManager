using AutoMapper;
using MediatR;
using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Application.Order.Commands
{
    public class DeleteOrderCommand : IRequest<TransactionOrderResponse>
    {
        public int Id { get; set; }
        public DeleteOrderCommand(int id)
            => Id = id;
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, TransactionOrderResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TransactionOrderResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrderRepository.GetOrder(request.Id);

            if (order is null)
                throw new ApplicationException("Order not found");

            _unitOfWork.OrderRepository.DeleteOrder(order);
            await _unitOfWork.Complete();

            return new()
            {
                OrderId = order.Id
            };
        }
    }
}
