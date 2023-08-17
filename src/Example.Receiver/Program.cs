using Azure.Messaging.ServiceBus;
using Example.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var configurationBuilder = new ConfigurationBuilder();

configurationBuilder.AddUserSecrets<Program>();

var configuration = configurationBuilder.Build();

var settings = new ServiceBusSettings();

configuration.GetSection(nameof(ServiceBusSettings)).Bind(settings);

var options = Options.Create(settings);

var serviceBusOptions = new ServiceBusClientOptions
{
    TransportType = ServiceBusTransportType.AmqpWebSockets
};

var client = new ServiceBusClient(options.Value.Uri, serviceBusOptions);

var processor = client.CreateProcessor("fieldmodel");

processor.ProcessMessageAsync += MessageHandler;
processor.ProcessErrorAsync += ErrorHandler;

await processor.StartProcessingAsync();

Console.ReadLine();

async Task MessageHandler(ProcessMessageEventArgs arguments)
{
    var body = arguments.Message.Body.ToString();
    var contentType = arguments.Message.ContentType;
    Console.WriteLine($"Received: {body} ({contentType})");

    await arguments.CompleteMessageAsync(arguments.Message);
}

// handle any errors when receiving messages
Task ErrorHandler(ProcessErrorEventArgs arguments)
{
    Console.WriteLine(arguments.Exception.ToString());
    return Task.CompletedTask;
}