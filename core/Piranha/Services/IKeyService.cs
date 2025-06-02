using Piranha.Models;

namespace Piranha.Services
{
    public interface IKeyService
    {
        Task<Key> GetByIdAsync(Guid id);
        
        Task<Key> GetByNameAsync(string name);

        Task<IEnumerable<Key>> GetAllAsync();

        Task<Key> SaveAsync(Key key);

        Task DeleteAsync(Guid id);

        Task DeleteAllAsync();
    }
}