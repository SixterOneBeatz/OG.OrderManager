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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrderManagerContext _context;

        public CustomerRepository(OrderManagerContext context)
            => _context = context;

        public void AddCustomer(Customer client)
            => _context.Customers.Add(client);

        public void DeleteCustomer(Customer client)
            => _context.Customers.Remove(client);

        public async Task<Customer> GetCustomer(int id)
            => await _context.Customers.FindAsync(id);

        public async Task<List<Customer>> GetCustomers()
            => await _context.Customers.ToListAsync();

        public void UpdateCustomer(Customer client)
        {
            _context.Customers.Attach(client);
            _context.Entry(client).State = EntityState.Modified;
        }
    }
}