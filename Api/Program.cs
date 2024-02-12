using MediatR;
using Persistance.PostgreSql.Config;
using App.Customers.Queries;
using App;
using Domain.Models;
using Serilog;
using Api.Middleware;
using Messenger.SQS.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPostgresql(builder.Configuration.GetConnectionString("CustomersPg")!);

builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection("Queue"));
builder.Services.AddSqsMessenger();

builder.Services.AddApp();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext());

builder.Services.AddScoped<ExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapGet("/api/customer", async (IMediator mediator, CancellationToken ct) =>
    await mediator.Send(CustomersQuery.Instance, ct))
.WithName("GetCustomers")
.WithOpenApi();

app.MapGet("/api/customer/{id}", async (Guid id, IMediator mediator, CancellationToken ct) =>
    await mediator.Send(new CustomerQuery(id), ct))
.WithName("GetCustomerById")
.WithOpenApi();

app.MapPost("/api/customer", async (IMediator mediator, Customer customer, CancellationToken ct) =>
    await mediator.Send(new CustomerCreateCommand{
        NewCustomer = customer
    }, ct))
.WithName("CreateCustomer")
.WithOpenApi();

app.MapPut("/api/customer", async (IMediator mediator, Customer customer, CancellationToken ct) =>
    await mediator.Send(new CustomerUpdateCommand{
        UpdatedCustomer = customer
    }, ct))
.WithName("UpdateCustomer")
.WithOpenApi();

app.MapDelete("/api/customer/{id}", async (Guid id, IMediator mediator, CancellationToken ct) =>
    await mediator.Send(new CustomerDeleteCommand{
        Id = id
    }, ct))
.WithName("DeleteCustomer")
.WithOpenApi();

app.Run();
