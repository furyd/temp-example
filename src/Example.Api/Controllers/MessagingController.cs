using Microsoft.AspNetCore.Mvc;
using Example.Domain.Commands.Models;
using Example.Domain.Settings;
using Microsoft.Extensions.Options;
using Azure.Messaging.ServiceBus;
using System.Text.Json;
using Example.Domain.Shared.Metrics.Interfaces;

namespace Example.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        private readonly ServiceBusSettings _settings;
        private readonly IMetrics _metrics;

        public MessagingController(IOptions<ServiceBusSettings> settings, IMetrics metrics)
        {
            _settings = settings.Value;
            _metrics = metrics;
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

            using(var _ = _metrics.TrackDuration(nameof(sender.SendMessageAsync)))
            {
                await sender.SendMessageAsync(message);
            }

            return Accepted();
        }
    }
}
