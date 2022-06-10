using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using OG.OrderManager.Application.Common.Protos;
using OG.OrderManager.Client.Components.Orders;
using OG.OrderManager.Client.Services.Customer;

namespace OG.OrderManager.Client.Components.Customers
{
    public partial class CustomersTable
    {
        [Inject] private ICustomerService _customerService { get; set; }
        [Inject] private IDialogService _dialogService { get; set; }
        [Inject] ISnackbar _snackbar { get; set; }

        private List<CustomerDTO> Customers = new();

        private string searchQuery = "";

        private CustomerDTO CurrentCustomer = null;

        protected override async Task OnInitializedAsync()
        {
            await GetCustomers();
        }

        private async Task GetCustomers()
        {
            var response = await _customerService.GetCustomers();

            Customers = response.Customers.ToList();            
        }

        private bool CustomerFilterFunc(CustomerDTO element) => CustomerFilterFunc(element, searchQuery);

        private bool CustomerFilterFunc(CustomerDTO element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{element.Id}".Contains(searchString))
                return true;
            return false;
        }

        private async Task OpenCustomerDialog(CustomerDTO customer)
        {
            DialogParameters parameters = new()
            {
                { "Customer", customer }
            };

            DialogOptions options = new()
            { 
                CloseOnEscapeKey = true, 
                DisableBackdropClick = true 
            };

            bool dialogResult = await _dialogService
                .Show<CustomerDialog>("Customer", parameters, options)
                .GetReturnValueAsync<bool>();

            if (dialogResult)
            {
                _snackbar.Add("Customer info was saved successfully", Severity.Success);
                await GetCustomers();
            }
        }

        private async Task ConfirmDelete(CustomerDTO customer)
        {
            try
            {
                DialogParameters parameters = new()
                {
                    { "Customer", customer },
                    { "IsDelete", true }
                };

                DialogOptions options = new()
                {
                    CloseOnEscapeKey = true,
                    DisableBackdropClick = true
                };

                bool dialogResult = await _dialogService
                    .Show<CustomerDialog>("Customer", parameters, options)
                    .GetReturnValueAsync<bool>();

                if (dialogResult)
                {
                    _snackbar.Add("Customer info was deleted successfully", Severity.Success);
                    await GetCustomers();
                }
            }
            catch (Exception ex)
            {
                await GetCustomers();
                Console.WriteLine(ex.Message);
            }
        }

        private void OpenOrdersDialog(CustomerDTO customer)
        {
            try
            {
                DialogParameters parameters = new()
                {
                    { "Customer", customer }
                };

                DialogOptions options = new()
                {
                    CloseOnEscapeKey = true,
                    DisableBackdropClick = true,
                    MaxWidth = MaxWidth.Large,
                    FullWidth = true,
                };

                _dialogService.Show<OrdersTable>($"{customer.Name} {customer.LastName}'s orders", parameters, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
