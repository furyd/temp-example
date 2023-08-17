using Microsoft.AspNetCore.Mvc;
using Example.Domain.Commands.Models;
using Example.Domain.Settings;
using Microsoft.Extensions.Options;
using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace Example.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        private readonly ServiceBusSettings _settings;

        public MessagingController(IOptions<ServiceBusSettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Send()
        {
            var options = new ServiceBusClientOptions
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            var client = new ServiceBusClient(_settings.Uri, options);

            var sender = client.CreateSender("fieldmodel");

            var message = new ServiceBusMessage(JsonSerializer.Serialize(new FieldModel("Testing")))
            {
                ContentType = System.Net.Mime.MediaTypeNames.Application.Json
            };

            await sender.SendMessageAsync(message);


            return Accepted();
        }
    }
}
