using System;

namespace Piranha.Events
{
    public class EventBus : IEventBus
    {
        public void Publish<T>(T @event)
        {
            // Basic console output for development/debugging
            Console.WriteLine($"[EVENT PUBLISHED] Type: {typeof(T).Name}");
            Console.WriteLine($"Event Data: {@event}");
            Console.WriteLine($"Published at: {DateTime.UtcNow}");
        }
    }
}
