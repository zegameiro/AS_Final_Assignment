using Piranha.Models;

namespace Piranha.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetAllAsync();
        Task<Subscription> GetByIdAsync(Guid id);
        Task<IEnumerable<Subscription>> GetByEventTypeAsync(string eventType);
        Task<IEnumerable<Subscription>> GetByEventStatusAsync(string eventStatus);
        Task<IEnumerable<Subscription>> GetByTagsAsync(string filter);
        Task<Subscription> SaveAsync(Subscription subscription);
        Task DeleteAsync(Guid id);
        Task DeleteAllAsync();
    }
}