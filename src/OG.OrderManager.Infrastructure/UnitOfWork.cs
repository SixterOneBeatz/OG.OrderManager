using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Infrastructure.Contexts;
using OG.OrderManager.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OG.OrderManager.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderManagerContext _context;
        private protected ICustomerRepository _customerRepository;
        private protected IOrderRepository _orderRepository;
        private protected IGreetRepository _greetRepository;

        public UnitOfWork(OrderManagerContext context)
            => _context = context;

        public ICustomerRepository CustomerRepository => _customerRepository = new CustomerRepository(_context);

        public IOrderRepository OrderRepository => _orderRepository = new OrderRepository(_context);

        public IGreetRepository GreetRepository => _greetRepository = new GreetRepository();

        public async Task<int> Complete()
            => await _context.SaveChangesAsync();
    }
}
