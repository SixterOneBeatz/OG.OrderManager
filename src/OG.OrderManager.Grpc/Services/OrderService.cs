using Grpc.Core;
using MediatR;
using OG.OrderManager.Application.Common.Protos;
using OG.OrderManager.Application.Order.Commands;
using OG.OrderManager.Application.Order.Queries;

namespace OG.OrderManager.Grpc.Services
{
    public class OrderService : Order.OrderBase
    {
        private readonly IMediator _mediator;

        public OrderService(IMediator mediator)
            => _mediator = mediator;

        public override async Task<OrdersDTO> GetOrdersByCustomer(GetOrdersByCustomerRequest request, ServerCallContext context)
            => await _mediator.Send(new GetOrdersByCustomerQuery(request.CustomerId));

        public override async Task<TransactionOrderResponse> AddOrder(OrderDTO request, ServerCallContext context)
            => await _mediator.Send(new AddOrderCommand(request));

        public override async Task<TransactionOrderResponse> UpdateOrder(OrderDTO request, ServerCallContext context)
            => await _mediator.Send(new UpdateOrderCommand(request));

        public override async Task<TransactionOrderResponse> DeleteOrder(DeleteOrderRequest request, ServerCallContext context)
            => await _mediator.Send(new DeleteOrderCommand(request.Id));

    }
}
