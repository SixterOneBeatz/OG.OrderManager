using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Components;
using OG.OrderManager.Application.Common.Protos;
using OG.OrderManager.Client.Services.Customer;

namespace OG.OrderManager.Client.Components.Customers
{
    public partial class CustomersTable
    {
        [Inject] public ICustomerService _customerService { get; set; }

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
    }
}
