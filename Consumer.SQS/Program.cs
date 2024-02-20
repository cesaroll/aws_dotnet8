using Amazon.SQS;
using Consumer.SQS.Config;
using Consumer.SQS.Handlers;
using Consumer.SQS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection("Queue"));
builder.Services.AddSingleton<IAmazonSQS, AmazonSQSClient>();
builder.Services.AddHostedService<QueueConsumerService>();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CustomerHandler>());

var app = builder.Build();

app.Run();

