namespace OG.OrderManager.Client.Services.Greeter
{
    public interface IGreeterService
    {
        Task<string> Greet(string name);
    }
}
