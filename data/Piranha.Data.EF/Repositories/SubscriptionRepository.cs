using Piranha.Models;
using Microsoft.EntityFrameworkCore;

namespace Piranha.Repositories
{
    internal class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IDb _db;

        public SubscriptionRepository(IDb db)
        {
            _db = db;
        }

        public Task DeleteAllAsync()
        {
            return _db.Subscriptions.ExecuteDeleteAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            return _db.Subscriptions
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            return await _db.Subscriptions
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> GetByEventTypeAsync(string eventType)
        {
            return await _db.Subscriptions
                .AsNoTracking()
                .Where(s => s.EventType == eventType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> GetByFilterAsync(string filter)
        {
            return await _db.Subscriptions
                .AsNoTracking()
                .Where(s => s.Filter == filter)
                .ToListAsync();
        }

        public async Task<Subscription> GetByIdAsync(Guid id)
        {
            return await _db.Subscriptions
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Subscription> SaveAsync(Subscription subscription)
        {
            if (subscription.Id == Guid.Empty)
            {
                subscription.Id = Guid.NewGuid();
                subscription.Created = DateTime.UtcNow;
                _db.Subscriptions.Add(subscription);
            }
            else
            {
                var dbSubscription = await _db.Subscriptions.FirstOrDefaultAsync(s => s.Id == subscription.Id);
                if (dbSubscription != null)
                {
                    dbSubscription.EventType = subscription.EventType;
                    dbSubscription.Filter = subscription.Filter;
                    dbSubscription.CallbackUrl = subscription.CallbackUrl;
                }
                else
                {
                    throw new KeyNotFoundException("Subscription not found.");
                }
            }

            await _db.SaveChangesAsync();
            return subscription;
        }
    }
}