using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Client.Services.Customer
{
    public interface ICustomerService
    {
        Task<CustomerDTO> GetCustomer(int id);
        Task<CustomersDTO> GetCustomers();
        Task AddCustomer(CustomerDTO customerDTO);
        Task UpdateCustomer(CustomerDTO customerDTO);
        Task DeleteCustomer(int id);
    }
}
