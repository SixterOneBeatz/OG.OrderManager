using AutoMapper;
using MediatR;
using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Application.Order.Commands
{
    public class UpdateOrderCommand : IRequest<TransactionOrderResponse>
    {
        public OrderDTO Order { get; set; }

        public UpdateOrderCommand(OrderDTO order)
            => Order = order;
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, TransactionOrderResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TransactionOrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrderRepository.GetOrder(request.Order.Id);

            if (order is null)
                throw new ApplicationException("Order not found");

            _unitOfWork.OrderRepository.UpdateOrder(order);
            _mapper.Map(request.Order, order);
            await _unitOfWork.Complete();

            return new()
            {
                OrderId = order.Id
            };
        }
    }
}
