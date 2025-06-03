using Piranha.Models;
using Piranha.Repositories;

namespace Piranha.Services
{
    internal sealed class SubscriptionService : ISubscriptionService
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

        public async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Subscription> GetByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Subscription>> GetByEventTypeAsync(string eventType)
        {
            return await _repo.GetByEventTypeAsync(eventType);
        }

        public async Task<IEnumerable<Subscription>> GetByEventStatusAsync(string eventStatus)
        {
            return await _repo.GetByEventStatusAsync(eventStatus);
        }

        public async Task<IEnumerable<Subscription>> GetByTagsAsync(string filter)
        {
            return await _repo.GetByTagsAsync(filter);
        }

        public async Task SaveAsync(Subscription subscription)
        {
            var possibleSubscription = await _repo.GetByEventTypeAsync(subscription.EventType);
            if (possibleSubscription != null)
            {
                // Check if any subscription with the same event type already exists
                foreach (var eSub in possibleSubscription)
                {
                    if (eSub.EventStatus == subscription.EventStatus && eSub.CallbackUrl.Contains(subscription.CallbackUrl))
                    {
                        throw new InvalidOperationException($"A subscription with the event type '{subscription.EventType}' and callback url '{subscription.CallbackUrl}' already exists.");
                    }
                }
            }

            await _repo.SaveAsync(subscription);
        }

        public async Task DeleteAsync(Guid id)
        {
            var subscription = await _repo.GetByIdAsync(id);
            if (subscription == null) throw new InvalidOperationException($"No subscription found with id '{id}'.");
            await _repo.DeleteAsync(id);
        }

        public async Task DeleteAllAsync()
        {
            await _repo.DeleteAllAsync();
        }
    }
}