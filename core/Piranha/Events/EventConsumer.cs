using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Piranha.Events;
using Piranha.Repositories;
using Piranha.Models;
using System.Linq;

public class EventConsumer : BackgroundService
{
    private readonly EventBus _eventBus;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly NotificationService _notificationService;

    public EventConsumer(EventBus eventBus, ISubscriptionRepository subscriptionRepository, NotificationService notificationService)
    {
        _eventBus = eventBus;
        _subscriptionRepository = subscriptionRepository;
        _notificationService = notificationService; 
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        await _eventBus.StartConsumingAsync(async @event =>
        {
            Console.WriteLine($"[EVENT RECEIVED] Type: {@event.Type}");

            // Get all the subscriptions for the event type
            var subscriptions = await _subscriptionRepository.GetByEventTypeAsync(@event.Status.ToString());

            if (subscriptions.Count() != 0)
            {
                Console.WriteLine("[EVENT CONSUMER] Found " + subscriptions.Count() + " subscriptions for event type: " + @event.Type);

                // Filter subscriptions by filter if provided
                var filtered = new List<Subscription>();
                foreach (var sub in subscriptions)
                {
                    if (string.IsNullOrEmpty(sub.Filter) || sub.Filter.Split(',').Any(tag => @event.Tags.Contains(tag.Trim())))
                    {
                        filtered.Add(sub);
                    }
                }

                if (filtered.Count > 0)
                {
                    Console.WriteLine("[EVENT CONSUMER] Found " + filtered.Count + " subscriptions after filtering by tags.");

                    // Notify each subscription
                    foreach (var subscription in filtered)
                    {
                        try
                        {
                            await _notificationService.NotifyAsync(subscription.CallbackUrl, @event);
                            Console.WriteLine($"[EVENT CONSUMER] Notified {subscription.CallbackUrl} for event type: {@event.Type}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[EVENT CONSUMER] Failed to notify {subscription.CallbackUrl}: {ex.Message}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("[EVENT CONSUMER] No subscriptions matched the filter for event type: " + @event.Type);
                }
            }

            await Task.CompletedTask;
        });
    }
}
