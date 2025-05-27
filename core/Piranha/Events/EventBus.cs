using System;

namespace Piranha.Events
{
    public class EventBus : IEventBus
    {
        public void Publish(Event @event)
        {
            // Basic console output for development/debugging
            Console.WriteLine($"[EVENT PUBLISHED] Type: {@event.Type.ToString()}");
            Console.WriteLine($"Event Status: {@event.Status}");
            Console.WriteLine($"Published at: {DateTime.UtcNow}");
        }
    }
}
