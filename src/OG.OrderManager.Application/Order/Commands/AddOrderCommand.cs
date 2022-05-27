using AutoMapper;
using MediatR;
using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Application.Order.Commands
{
    public class AddOrderCommand : IRequest<TransactionOrderResponse>
    {
        public OrderDTO Order { get; set; }

        public AddOrderCommand(OrderDTO order)
            => Order = order;
    }

    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, TransactionOrderResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TransactionOrderResponse> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            Domain.Order order = _mapper.Map<Domain.Order>(request.Order);

            _unitOfWork.OrderRepository.AddOrder(order);
            await _unitOfWork.Complete();

            return new()
            {
                OrderId = order.Id
            };
        }
    }
}
