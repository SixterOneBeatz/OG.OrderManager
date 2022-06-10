using Google.Protobuf.WellKnownTypes;
using OG.OrderManager.Application.Common.Protos;
using ApplicationProtos = OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Client.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationProtos.Customer.CustomerClient _customerClient;

        public CustomerService(IServiceProvider serviceProvider)
            => _customerClient = serviceProvider.GetRequiredService<ApplicationProtos.Customer.CustomerClient>();

        public async Task<TransactionCustomerResponse> AddCustomer(CustomerDTO customerDTO)
            => await _customerClient.AddCustomerAsync(customerDTO);

        public async Task<TransactionCustomerResponse> DeleteCustomer(int id)
            => await _customerClient.DeleteCustomerAsync(new CustomerRequest() { Id = id});

        public async Task<CustomerDTO> GetCustomer(int id)
            => await _customerClient.GetCustomerAsync(new CustomerRequest() { Id = id });

        public async Task<CustomersDTO> GetCustomers()
            => await _customerClient.GetCustomersAsync(new Empty());

        public async Task<TransactionCustomerResponse> UpdateCustomer(CustomerDTO customerDTO)
            => await _customerClient.UpdateCustomerAsync(customerDTO);
    }
}
