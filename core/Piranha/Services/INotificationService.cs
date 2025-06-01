namespace Piranha.Services
{
    public interface INotificationService
    {
        Task NotifyAsync(string callbackUrl, object payload);
    }
}