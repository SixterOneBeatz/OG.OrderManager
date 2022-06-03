using Microsoft.AspNetCore.Components;
using OG.OrderManager.Client.Services.Greeter;
namespace OG.OrderManager.Client.Pages
{
    public partial class GrpcExample
    {
        [Inject] public IGreeterService _greeterService { get; set; }

        private string name = string.Empty;
        private string inputValue = string.Empty;

        private async Task CallService()
        {
            try
            {
                name = await _greeterService.Greet(inputValue);
            }
            catch (Exception ex)
            {
                name = ex.Message;
            }
        }

    }
}
