using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using OG.OrderManager.Application.Common.Protos;
using OG.OrderManager.Client;
using OG.OrderManager.Client.Services.Customer;
using OG.OrderManager.Client.Services.Greeter;
using OG.OrderManager.Client.Services.Order;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#region Backend config
string backendUrl = builder.Configuration["GrpcService"]!;
HttpClient httpClient = new(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
GrpcChannelOptions options = new() { HttpClient = httpClient };
GrpcChannel channel = GrpcChannel.ForAddress(backendUrl, options);
#endregion

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton(services => new Greeter.GreeterClient(channel));
builder.Services.AddSingleton(services => new Customer.CustomerClient(channel));
builder.Services.AddSingleton(services => new Order.OrderClient(channel));

builder.Services.AddSingleton<IGreeterService, GreeterService>();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddSingleton<IOrderService, OrderService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
