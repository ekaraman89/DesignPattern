using DesignPattern;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IShippingStrategyFactory, ShippingStrategyFactory>();
builder.Services.AddKeyedScoped<IShippingStrategy, UPSShippingStrategy>("UPS");
builder.Services.AddKeyedScoped<IShippingStrategy, DHLShippingStrategy>("DHL");
builder.Services.AddKeyedScoped<IShippingStrategy, FedexShippingStrategy>("Fedex");
builder.Services.AddKeyedScoped<IShippingStrategy, ArasShippingStrategy>("Aras");
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/weatherforecast", (
        [FromServices] IShippingStrategyFactory shippingStrategyFactory,
        Order order) =>
    {
     var shipping =   shippingStrategyFactory.GetShippingStrategy(order);
    var cost = shipping.CalculateShippingCost(order);
    return cost;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();