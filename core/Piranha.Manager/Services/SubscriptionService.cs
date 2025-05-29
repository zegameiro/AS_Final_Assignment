using Piranha.Models;
using Piranha.Repositories;
using Piranha.Services;

namespace Piranha.Manager.Services
{
    public class SubscriptionService
    {
        private readonly ISubscriptionRepository _repo;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="repo">The main repository</param>
        public SubscriptionService(ISubscriptionRepository repo)
        {
            _repo = repo;
        }

        /// <inheritdoc />
        public Task<IEnumerable<Subscription>> GetAllAsync() => _repo.GetAllAsync();

        /// <inheritdoc />
        public Task<Subscription> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

        /// <inheritdoc />
        public Task<IEnumerable<Subscription>> GetByEventTypeAsync(string eventType) => _repo.GetByEventTypeAsync(eventType);

        /// <inheritdoc />
        public Task<IEnumerable<Subscription>> GetByFilterAsync(string filter) => _repo.GetByFilterAsync(filter);

        /// <inheritdoc />
        public Task<Subscription> SaveAsync(Subscription subscription) => _repo.SaveAsync(subscription);

        /// <inheritdoc />
        public Task DeleteAsync(Guid id) => _repo.DeleteAsync(id);

        /// <inheritdoc />
        public Task DeleteAllAsync() => _repo.DeleteAllAsync();
    }
}

