using AutoMapper;
using Google.Protobuf.Collections;
using MediatR;
using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Application.Common.Protos;


namespace OG.OrderManager.Application.Customer.Queries
{
    public class GetCustomersQuery : IRequest<CustomersDTO> { }

    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, CustomersDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomersDTO> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _unitOfWork.CustomerRepository.GetCustomers();

            CustomersDTO response = new();
            response.Customers.Add(_mapper.Map<RepeatedField<CustomerDTO>>(customers));

            return response;
        }
    }
}
