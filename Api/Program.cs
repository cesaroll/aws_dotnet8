using MediatR;
using Persistance.PostgreSql.Config;
using App.Customers.Queries;
using App;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPostgresql(builder.Configuration.GetConnectionString("CustomersPg")!);

builder.Services.AddApp();

builder.Services.AddApp();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/customers", async (IMediator mediator) => {
    var customers = await mediator.Send(GetAllCustomersQuery.Instance);
    return customers;
})
.WithName("GetCustomers")
.WithOpenApi();

app.MapGet("/api/customers/{id}", async (Guid id, IMediator mediator) => {
    var query = new GetByIdCustomerQuery(id);
    var customers = await mediator.Send(GetAllCustomersQuery.Instance);
    return customers;
})
.WithName("GetCustomerById")
.WithOpenApi();

app.Run();
