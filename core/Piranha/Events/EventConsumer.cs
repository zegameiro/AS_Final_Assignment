using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Piranha.Events;

public class EventConsumer : BackgroundService
{
    private readonly EventBus _eventBus;
    private static readonly List<Event> ConsumedEvents = new List<Event>();
    private static readonly object Lock = new object();

    public EventConsumer(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        await _eventBus.StartConsumingAsync(async @event =>
        {
            Console.WriteLine($"[EVENT RECEIVED] Type: {@event.Type}");
            lock (Lock)
            {
                ConsumedEvents.Add(@event);
            }
            await Task.CompletedTask;
        });
    }

    public static IEnumerable<Event> GetConsumedEvents()
    {
        lock (Lock)
        {
            // Return a copy to avoid issues with concurrent modification if the list is iterated elsewhere
            return ConsumedEvents.ToList();
        }
    }
}
