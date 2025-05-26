public interface IEventBus
{
    void Publish<T>(T @event);
}