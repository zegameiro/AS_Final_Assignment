using Piranha.Repositories;
using Piranha.Models;

namespace Piranha.Manager.Services
{
    public class KeyService
    {
        private readonly IKeyRepository _repo;

        public KeyService(IKeyRepository repo)
        {
            _repo = repo;
        }

        public Task<Key> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

        public Task<Key> GetByNameAsync(string name) => _repo.GetByNameAsync(name);

        public Task<IEnumerable<Key>> GetAllAsync() => _repo.GetAllAsync();

        public Task<Key> SaveAsync(Key key) => _repo.SaveAsync(key);

        public Task DeleteAsync(Guid id) => _repo.DeleteAsync(id);

        public Task DeleteAllAsync() => _repo.DeleteAllAsync();
    }
}