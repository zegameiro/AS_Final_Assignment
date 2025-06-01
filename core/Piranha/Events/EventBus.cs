using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Piranha.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Piranha.Events
{
    public class EventBus : IAsyncDisposable
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private const string QueueName = "events";

        private EventBus(IConnection connection, IChannel channel)
        {
            _connection = connection;
            _channel = channel;
        }

        public static async Task<EventBus> CreateAsync(string hostName = "localhost")
        {
            var factory = new ConnectionFactory { HostName = hostName };
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            // Declare queue asynchronously
            await channel.QueueDeclareAsync(queue: QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            return new EventBus(connection, channel);
        }

        public async Task Publish(Event @event)
        {
            var json = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(json);

            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: QueueName, body: body);

            Console.WriteLine($"[EVENT PUBLISHED] Type: {@event.Type}");
        }

        public async Task StartConsumingAsync(Func<Event, Task> onMessageReceived)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                
                try
                {
                    var @event = JsonSerializer.Deserialize<Event>(json);
                    if (@event != null)
                    {
                        await onMessageReceived(@event); // Custom message handler
                    }

                    // Acknowledge the message after processing
                    await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                    // Optionally: Nack and requeue, or just nack without requeue based on your logic
                    await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                }
            };

            // Start consuming messages
            await _channel.BasicConsumeAsync(queue: QueueName, autoAck: false, consumer: consumer);

            // This method won't complete unless you explicitly handle shutdown or cancellation
            await Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel != null)
            {
                await _channel.CloseAsync();
                await _channel.DisposeAsync();
            }

            if (_connection != null)
            {
                await _connection.CloseAsync();
                await _connection.DisposeAsync();
            }
        }
    }
}
