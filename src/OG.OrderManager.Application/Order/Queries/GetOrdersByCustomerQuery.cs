using AutoMapper;
using Google.Protobuf.Collections;
using MediatR;
using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Application.Order.Queries
{
    public class GetOrdersByCustomerQuery : IRequest<OrdersDTO>
    {
        public int CustomerId { get; set; }

        public GetOrdersByCustomerQuery(int customerId)
            => CustomerId = customerId;

    }

    public class GetOrdersByCustomerQueryHandler : IRequestHandler<GetOrdersByCustomerQuery, OrdersDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrdersByCustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrdersDTO> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = _unitOfWork.CustomerRepository.GetCustomer(request.CustomerId);

            if (customer is null)
                throw new ApplicationException("Customer not found");

            var orders = await _unitOfWork.OrderRepository.GetOrdersByCustomer(request.CustomerId);

            var response = new OrdersDTO();
            response.Orders.Add(_mapper.Map<RepeatedField<OrderDTO>>(orders));

            return response;
        }
    }
}
