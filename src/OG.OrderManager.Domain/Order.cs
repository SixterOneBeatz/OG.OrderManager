using OG.OrderManager.Domain.Common;

namespace OG.OrderManager.Domain
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
