using Piranha.Models;

namespace Piranha.Services
{
    public interface ISubscriptionService
    {
        /// <summary>
        /// Gets all subscriptions.
        /// </summary>
        /// <returns>The available subscriptions</returns>
        Task<IEnumerable<Subscription>> GetAllAsync();

        /// <summary>
        /// Gets the subscription with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        /// <returns>The subscription</returns>
        Task<Subscription> GetByIdAsync(Guid id);

        Task<IEnumerable<Subscription>> GetByEventTypeAsync(string eventType);

        Task<IEnumerable<Subscription>> GetByEventStatusAsync(string eventStatus);

        Task<IEnumerable<Subscription>> GetByTagsAsync(string filter);

        /// <summary>
        /// Adds or updates the given subscription in the database
        /// depending on its state.
        /// </summary>
        /// <param name="subscription">The subscription</param>
        Task SaveAsync(Subscription subscription);

        /// <summary>
        /// Deletes the subscription with the specified id.
        /// </summary>
        /// <param name="id">The unique id</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Deletes all subscriptions.
        /// </summary>
        Task DeleteAllAsync();
    }
}