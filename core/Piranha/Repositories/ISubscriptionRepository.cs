using Piranha.Models;

namespace Piranha.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetAllAsync();
        Task<Subscription> GetByIdAsync(Guid id);
        Task<IEnumerable<Subscription>> GetByEventTypeAsync(string eventType);
        Task<IEnumerable<Subscription>> GetByFilterAsync(string filter);
        Task<Subscription> SaveAsync(Subscription subscription);
        Task DeleteAsync(Guid id);
        Task DeleteAllAsync();
    }
}