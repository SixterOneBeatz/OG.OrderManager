using OG.OrderManager.Domain.Common;

namespace OG.OrderManager.Domain
{
    public class Customer : BaseEntity
    {
        public Customer()
            => Orders = new HashSet<Order>();

        public string Name { get; set; }
        public string LastName { get; set; }
        public string RFC { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
