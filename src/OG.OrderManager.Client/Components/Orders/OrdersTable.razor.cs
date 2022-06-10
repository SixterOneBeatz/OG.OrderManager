using Microsoft.AspNetCore.Components;
using MudBlazor;
using OG.OrderManager.Application.Common.Protos;
using OG.OrderManager.Client.Services.Order;

namespace OG.OrderManager.Client.Components.Orders
{
    public partial class OrdersTable
    {
        [Inject] private IOrderService _orderService { get; set; }
        [Inject] private ISnackbar _snackbar { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public CustomerDTO Customer { get; set; }

        private List<OrderDTO> Orders = new();
        private string searchQuery = "";
        private OrderDTO CurrentOrder = null;

        protected override async Task OnInitializedAsync()
        {
            await GetOrders();
        }

        private async Task GetOrders()
        {
            try
            {
                Orders.Clear();
                var request = await _orderService.GetOrdersByCustomer(Customer.Id);
                Orders = request.Orders.ToList();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
            }
        }

        private bool OrderFilterFunc(OrderDTO element) => OrderFilterFunc(element, searchQuery);

        private bool OrderFilterFunc(OrderDTO element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{element.Amount}".Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{element.Id}".Contains(searchString))
                return true;
            return false;
        }

        private void AddRow()
        {
            Orders.Insert(0, new() { CustomerId = Customer.Id });
        }

        private async Task SubmitOrder(OrderDTO order)
        {
            try
            {
                TransactionOrderResponse result;
                if (order.Id == 0)
                    result = await _orderService.AddOrder(order);
                else
                    result = await _orderService.UpdateOrder(order);

                if (result.OrderId > 0)
                    _snackbar.Add("Order info was saved successfully", Severity.Success);
                
                order.Id = result.OrderId;
                await GetOrders();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await GetOrders();
            }
        }

        private async Task DeleteOrder(OrderDTO order)
        {
            try
            {
                bool? result = await DialogService.ShowMessageBox(
                "Warning",
                "Deleting can not be undone!",
                yesText: "Delete!",
                cancelText: "Cancel");

                if (result.HasValue)
                {
                    TransactionOrderResponse serviceResult;
                    if (order.Id == 0)
                        Orders.Remove(order);
                    else
                        serviceResult = await _orderService.DeleteOrder(order.Id);

                    _snackbar.Add("Order info was deleted successfully", Severity.Success);
                    await GetOrders();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await GetOrders();
            }
        }

        private async Task Close()
        {
            try
            {
                Orders.Clear();
                Orders = new();
                StateHasChanged();
                MudDialog.Close();
            }
            catch (Exception ex)
            {
                Orders = new();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
