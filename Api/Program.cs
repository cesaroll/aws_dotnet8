using MediatR;
// using Persistance.PostgreSql.Config;
using Persistance.DynamoDb.Config;
using App.Customers.Queries;
using App.Config;
using Serilog;
using Api.Middleware;
// using Messenger.SQS.Config;
using Messenger.SNS.Config;
using Api.Dtos;
using Api.Mappers;
using App.Customers.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddPostgresql(builder.Configuration.GetConnectionString("CustomersPg")!);
builder.Services.AddDynamoDb();

// builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection("Queue"));
// builder.Services.AddSqsMessenger();
builder.Services.Configure<SnsSettings>(builder.Configuration.GetSection("Sns"));
builder.Services.AddSnsMessenger();

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

// app.MapGet("/api/customer/{id}", async (Guid id, IMediator mediator, CancellationToken ct) =>
//     await mediator.Send(new CustomerQuery(id), ct))
// .WithName("GetCustomerById")
// .WithOpenApi();

app.MapGet("/api/customer/{isOrEmail}", async (string idOrEmail, IMediator mediator, CancellationToken ct) =>
    await mediator.Send(new CustomerQuery(idOrEmail), ct))
.WithName("GetCustomer")
.WithOpenApi();

app.MapPost("/api/customer", async (IMediator mediator, CreateCustomerDto createCustomerDto, CancellationToken ct) =>
    await mediator.Send(new CustomerCreateCommand{
        NewCustomer = createCustomerDto.ToCustomer()
    }, ct))
.WithName("CreateCustomer")
.WithOpenApi();

app.MapPut("/api/customer/{id}", async (IMediator mediator, Guid id, UpdateCustomerDto updateCustomerDto, CancellationToken ct) =>
    await mediator.Send(new CustomerUpdateCommand{
        UpdatedCustomer = updateCustomerDto.ToCustomer(id)
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
