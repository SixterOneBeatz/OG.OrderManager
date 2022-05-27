namespace OG.OrderManager.Application.Common.Interfaces
{
    public interface IOrderRepository
    {
        Task<Domain.Order> GetOrder(int id);
        Task<IEnumerable<Domain.Order>> GetOrdersByCustomer(int customerId);
        void AddOrder(Domain.Order order);
        void UpdateOrder(Domain.Order order);
        void DeleteOrder(Domain.Order order);
    }
}
