using OG.OrderManager.Application;
using OG.OrderManager.Grpc.Services;
using OG.OrderManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddCors(o => o.AddPolicy("AllowAll", bldr =>
{
    string origin = builder.Configuration["ClientUri"];
    bldr.WithOrigins(origin)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.UseGrpcWeb(new() { DefaultEnabled = true });
app.UseCors();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<GreeterService>()
             .EnableGrpcWeb()
             .RequireCors("AllowAll");
});

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
