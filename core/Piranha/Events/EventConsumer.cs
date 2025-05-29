using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Piranha.Events;

public class EventConsumer : BackgroundService
{
    private readonly EventBus _eventBus;

    public EventConsumer(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        await _eventBus.StartConsumingAsync(async @event =>
        {
            Console.WriteLine($"[EVENT RECEIVED] Type: {@event.Type}");

            await Task.CompletedTask;
        });
    }
}
