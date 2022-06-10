using Microsoft.AspNetCore.Components;
using MudBlazor;
using OG.OrderManager.Application.Common.Protos;
using OG.OrderManager.Client.Services.Customer;

namespace OG.OrderManager.Client.Components.Customers
{
    public partial class CustomerDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public CustomerDTO Customer { get; set; }
        [Parameter] public bool IsDelete { get; set; } = false;
        [Inject] private ICustomerService _customerService { get; set; }
        private async Task Submit()
        {
            TransactionCustomerResponse response;

            if (Customer.Id == 0)
                response = await _customerService.AddCustomer(Customer);
            else
                response = await _customerService.UpdateCustomer(Customer);

            if (response.Id > 0)
                MudDialog.Close(DialogResult.Ok(true));
            else
                MudDialog.Close(DialogResult.Ok(false));
        }

        private async Task Delete()
        {
            var response = await _customerService.DeleteCustomer(Customer.Id);

            if (response.Id > 0)
                MudDialog.Close(DialogResult.Ok(true));
            else
                MudDialog.Close(DialogResult.Ok(false));
        }

        void Cancel() => MudDialog.Close(DialogResult.Ok(false));

    }
}
