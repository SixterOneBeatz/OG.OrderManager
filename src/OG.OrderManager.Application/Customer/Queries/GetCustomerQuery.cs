using AutoMapper;
using MediatR;
using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Application.Customer.Queries
{
    public class GetCustomerQuery : IRequest<CustomerDTO>
    {
        public int Id { get; set; }

        public GetCustomerQuery(int id)
            => Id = id;
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerDTO> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            Domain.Customer customer = await _unitOfWork.CustomerRepository.GetCustomer(request.Id);

            if (customer is null)
                throw new ApplicationException("Not found");

            return _mapper.Map<CustomerDTO>(customer);
        }
    }
}
