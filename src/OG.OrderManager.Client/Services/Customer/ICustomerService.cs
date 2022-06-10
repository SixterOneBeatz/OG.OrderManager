using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Client.Services.Customer
{
    public interface ICustomerService
    {
        Task<CustomerDTO> GetCustomer(int id);
        Task<CustomersDTO> GetCustomers();
        Task<TransactionCustomerResponse> AddCustomer(CustomerDTO customerDTO);
        Task<TransactionCustomerResponse> UpdateCustomer(CustomerDTO customerDTO);
        Task<TransactionCustomerResponse> DeleteCustomer(int id);
    }
}
