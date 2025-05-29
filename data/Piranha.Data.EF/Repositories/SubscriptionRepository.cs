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
            return _db.Subscriptions
                .ExecuteDeleteAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            return _db.Subscriptions
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();
        }

        public Task<IEnumerable<Subscription>> GetAllAsync()
        {
            return _db.Subscriptions
                .AsNoTracking()
                .ToListAsync()
                .ContinueWith(t => (IEnumerable<Models.Subscription>)t.Result);
        }

        public Task<IEnumerable<Subscription>> GetByEventTypeAsync(string eventType)
        {
            return _db.Subscriptions
                .AsNoTracking()
                .Where(s => s.EventType == eventType)
                .ToListAsync()
                .ContinueWith(t => (IEnumerable<Subscription>)t.Result);
        }

        public Task<IEnumerable<Subscription>> GetByFilterAsync(string filter)
        {
            return _db.Subscriptions
                .AsNoTracking()
                .Where(s => s.Filter == filter)
                .ToListAsync()
                .ContinueWith(t => (IEnumerable<Subscription>)t.Result);
        }

        public Task<Subscription> GetByIdAsync(Guid id)
        {
            return _db.Subscriptions
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id)
                .ContinueWith(t => t.Result != null ? new Subscription
                {
                    Id = t.Result.Id,
                    EventType = t.Result.EventType,
                    Filter = t.Result.Filter,
                    CallbackUrl = t.Result.CallbackUrl,
                    Created = t.Result.Created
                } : null);
        }

        public Task<Subscription> SaveAsync(Subscription subscription)
        {
            if (subscription.Id == Guid.Empty)
            {
                Console.WriteLine("Adding new subscription in Repository.");
                subscription.Id = Guid.NewGuid();
                _db.Subscriptions.Add(subscription);
                Console.WriteLine("Subscription added with success in Repository.");
                
            }
            else
            {
                var dbSubscription = _db.Subscriptions
                    .FirstOrDefault(s => s.Id == subscription.Id);

                if (dbSubscription != null)
                {
                    dbSubscription.EventType = subscription.EventType;
                    dbSubscription.Filter = subscription.Filter;
                    dbSubscription.CallbackUrl = subscription.CallbackUrl;
                    dbSubscription.Created = subscription.Created;
                }
                else
                {
                    throw new KeyNotFoundException("Subscription not found.");
                }
            }

            return _db.SaveChangesAsync()
                .ContinueWith(t => subscription);
        }
    }
}