using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using OG.OrderManager.Application.Common.Protos;
using OG.OrderManager.Application.Customer.Commands;
using OG.OrderManager.Application.Customer.Queries;

namespace OG.OrderManager.Grpc.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly IMediator _mediator;

        public CustomerService(IMediator mediator)
            => _mediator = mediator;

        public override async Task<CustomersDTO> GetCustomers(Empty request, ServerCallContext context)
            => await _mediator.Send(new GetCustomersQuery());

        public override async Task<CustomerDTO> GetCustomer(CustomerRequest request, ServerCallContext context)
            => await _mediator.Send(new GetCustomerQuery(request.Id));

        public override async Task<TransactionCustomerResponse> AddCustomer(CustomerDTO request, ServerCallContext context)
            => await _mediator.Send(new AddCustomerCommand(request));

        public override async Task<TransactionCustomerResponse> UpdateCustomer(CustomerDTO request, ServerCallContext context)
            => await _mediator.Send(new UpdateCustomerCommand(request));

        public override async Task<TransactionCustomerResponse> DeleteCustomer(CustomerRequest request, ServerCallContext context)
            => await _mediator.Send(new DeleteCustomerCommand(request.Id));

    }
}
