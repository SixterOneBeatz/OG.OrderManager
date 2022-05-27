namespace OG.OrderManager.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IOrderRepository OrderRepository { get; }
        IGreetRepository GreetRepository { get; }
        Task<int> Complete();
    }
}
