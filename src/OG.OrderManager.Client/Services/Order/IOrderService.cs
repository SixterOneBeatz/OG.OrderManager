using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Client.Services.Order
{
    public interface IOrderService
    {
        Task<OrdersDTO> GetOrdersByCustomer(int customerId);
        Task<TransactionOrderResponse> AddOrder(OrderDTO order);
        Task<TransactionOrderResponse> UpdateOrder(OrderDTO order);
        Task<TransactionOrderResponse> DeleteOrder(int id);
    }
}
