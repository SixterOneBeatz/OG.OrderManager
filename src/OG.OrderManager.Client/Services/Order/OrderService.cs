using OG.OrderManager.Application.Common.Protos;
using ApplicationProtos = OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Client.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationProtos.Order.OrderClient _client;

        public OrderService(ApplicationProtos.Order.OrderClient client)
            => _client = client;
            
        public async Task<TransactionOrderResponse> AddOrder(OrderDTO order)
            => await _client.AddOrderAsync(order);

        public async Task<TransactionOrderResponse> DeleteOrder(int id)
            => await _client.DeleteOrderAsync(new() { Id = id });

        public async Task<OrdersDTO> GetOrdersByCustomer(int customerId)
            => await _client.GetOrdersByCustomerAsync(new() { CustomerId = customerId });

        public async Task<TransactionOrderResponse> UpdateOrder(OrderDTO order)
            => await _client.UpdateOrderAsync(order);
    }
}
