namespace OG.OrderManager.Application.Common.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Domain.Customer>> GetCustomers();
        Task<Domain.Customer> GetCustomer(int id);
        void AddCustomer(Domain.Customer client);
        void UpdateCustomer(Domain.Customer client);
        void DeleteCustomer(Domain.Customer client);
    }
}
