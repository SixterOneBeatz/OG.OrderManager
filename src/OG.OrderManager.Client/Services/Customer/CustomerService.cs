using Google.Protobuf.WellKnownTypes;
using ApplicationProtos = OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Client.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationProtos.Customer.CustomerClient _customerClient;

        public CustomerService(IServiceProvider serviceProvider)
        {
            _customerClient = serviceProvider.GetRequiredService<ApplicationProtos.Customer.CustomerClient>();
        }

        public Task AddCustomer(ApplicationProtos.CustomerDTO customerDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationProtos.CustomerDTO> GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationProtos.CustomersDTO> GetCustomers()
            => await _customerClient.GetCustomersAsync(new Empty());

        public Task UpdateCustomer(ApplicationProtos.CustomerDTO customerDTO)
        {
            throw new NotImplementedException();
        }
    }
}
