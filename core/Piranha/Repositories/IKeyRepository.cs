using Piranha.Models;

namespace Piranha.Repositories
{
    public interface IKeyRepository
    {
        Task<Key> GetByIdAsync(Guid id);
        Task<Key> GetByNameAsync(string name);
        Task<IEnumerable<Key>> GetAllAsync();
        Task<Key> SaveAsync(Key key);
        Task DeleteAsync(Guid id);
        Task DeleteAllAsync();

    }
}
