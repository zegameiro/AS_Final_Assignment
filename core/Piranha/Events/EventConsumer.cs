using System.Collections.Generic;
using System.Linq;
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
    private static readonly List<Event> ConsumedEvents = new List<Event>();
    private static readonly object Lock = new object();
    private readonly IMediaRepository _mediaRepository;
    private readonly IPageRepository _pageRepository;

    public EventConsumer(
        EventBus eventBus,
        ISubscriptionRepository subscriptionRepository,
        NotificationService notificationService,
        IMediaRepository mediaRepository,
        IPageRepository pageRepository
    )
    {
        _eventBus = eventBus;
        _subscriptionRepository = subscriptionRepository;
        _notificationService = notificationService;
        _mediaRepository = mediaRepository;
        _pageRepository = pageRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        await _eventBus.StartConsumingAsync(async @event =>
        {
            Console.WriteLine($"[EVENT RECEIVED] Type: {@event.Type}");
            Console.WriteLine($"[EVENT RECEIVED] Status: {@event.Status}");

            // Get all the subscriptions for the event type
            var subscriptions = await _subscriptionRepository.GetByEventTypeAsync(@event.Type.ToString());
        
            if (subscriptions.Any())
            {
                Console.WriteLine("[EVENT CONSUMER] Found " + subscriptions.Count() + " subscriptions for event type: " + @event.Type);

                var subs = new List<Subscription>();
                foreach (var sub in subscriptions)
                {
                    if (sub.EventStatus == @event.Status.ToString())
                    {
                        subs.Add(sub);
                    }
                }

                Console.WriteLine("[EVENT CONSUMER] Found " + subs.Count + " subscriptions for event status: " + @event.Status);
                
                if (subs.Count != 0)
                {
                    // Filter subscriptions by filter if provided
                    var filtered = new List<Subscription>();
                    foreach (var sub in subs)
                    {
                        if (sub.Tags.Contains(','))
                        {
                            if (sub.Tags.Split(',').Any(tag => @event.Tags.Contains(tag.Trim())))
                            {
                                filtered.Add(sub);
                            }
                        }
                        else if (@event.Tags.Contains(sub.Tags.Trim()))
                        {
                            filtered.Add(sub);
                        }
                    }

                    if (filtered.Count > 0)
                    {
                        // Notify each subscription
                        foreach (var subscription in filtered)
                        {
                            object content = null;

                            if (@event.Type == EventType.Page)
                            {
                                content = await _pageRepository.GetById<PageInfo>(@event.ContentId);
                            }
                            else if (@event.Type == EventType.Media)
                            {
                                content = await _mediaRepository.GetById(@event.ContentId);
                            }
                            try
                            {
                                var sendEvent = new
                                {
                                    @event.Id,
                                    @event.CreatedAt,
                                    Type = @event.Type.ToString(),
                                    Status = @event.Status.ToString(),
                                    @event.ContentId,
                                    @event.Tags
                                };

                                var payload = new
                                {
                                    Event = sendEvent,
                                    Content = content,
                                };
                                await _notificationService.NotifyAsync(subscription.CallbackUrl, payload);
                                Console.WriteLine($"[EVENT CONSUMER] Notified {subscription.CallbackUrl} for event status: {@event.Status}");
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
            }
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
