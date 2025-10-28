using BugStore.Interfaces;
using BugStore.Models;
using BugStore.Repositories;
using BugStore.settings;
using CustomerEndpoints;
using ProductEndpoints;
using OrderEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServerDbContext(builder.Configuration);
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCustomerEndpoints();
app.MapProductEndpoints();
app.MapOrderEndpoints();

app.Run();
