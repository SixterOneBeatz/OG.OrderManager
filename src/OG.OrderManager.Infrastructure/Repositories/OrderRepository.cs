using Microsoft.EntityFrameworkCore;
using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Domain;
using OG.OrderManager.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OG.OrderManager.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderManagerContext _context;

        public OrderRepository(OrderManagerContext context)
            => _context = context;

        public void AddOrder(Order order)
            => _context.Orders.Add(order);

        
        public void DeleteOrder(Order order)
            => _context.Orders.Remove(order);

        public async Task<Order> GetOrder(int id)
            => await  _context.Orders.FindAsync(id);

        public async Task<IEnumerable<Order>> GetOrdersByCustomer(int customerId)
            => await _context.Orders.ToListAsync();

        public void UpdateOrder(Order order)
        {
            _context.Orders.Attach(order);
            _context.Entry(order).State = EntityState.Modified;
        }
    }
}
