using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Domain.Messages;


Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine((char)9201);

var cts = new CancellationTokenSource();
var sqlClient = new AmazonSQSClient();

var queueUrlResponse =await sqlClient.GetQueueUrlAsync("customers", cts.Token);

var receiveMessageRequest = new ReceiveMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    AttributeNames = new List<string> { "All" },
    MessageAttributeNames = new List<string> { "All" }
};


while(!cts.Token.IsCancellationRequested)
{
    var response = await sqlClient.ReceiveMessageAsync(receiveMessageRequest, cts.Token);

    foreach(var message in response.Messages)
    {
        try
        {
            Console.WriteLine($"Message Id: {message.MessageId}");
            Console.WriteLine($"Message Body: {message.Body}");

            var messageType = message.MessageAttributes["MessageType"].StringValue;
            var type = Type.GetType($"Domain.Messages.{messageType}, Domain");

            if (type is null)
            {
                Console.WriteLine("Invalid MessageType: {messageType}", messageType);
                continue;
            }

            Console.WriteLine($"MessageType: {messageType}");

            var obj = JsonSerializer.Deserialize(message.Body, type) as IMessage; // TODO: Mediatr

            await sqlClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle);

        } catch(Exception ex)
        {
            Console.WriteLine($"Error processing message: {message}");
            Console.WriteLine(ex.Message);
        }
    }

    Console.WriteLine((char)9201);
    await Task.Delay(3000);
}
