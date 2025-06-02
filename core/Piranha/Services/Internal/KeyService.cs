using Piranha.Repositories;
using Piranha.Models;
using Piranha.Services;

namespace Piranha.Services
{
    public class KeyService : IKeyService
    {
        private readonly IKeyRepository _repo;

        public KeyService(IKeyRepository repo)
        {
            _repo = repo;
        }

        public Task<Key> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

        public Task<Key> GetByNameAsync(string name) => _repo.GetByNameAsync(name);

        public Task<IEnumerable<Key>> GetAllAsync() => _repo.GetAllAsync();

        public async Task<Key> SaveAsync(Key key)
        {
            var possibleKey = await _repo.GetByNameAsync(key.Name);

            if (possibleKey != null)
                throw new InvalidOperationException($"A key with the name '{key.Name}' already exists.");
                
            await _repo.SaveAsync(key);
            return key;
        }

        public Task DeleteAsync(Guid id)
        {
            var possibleKey = _repo.GetByIdAsync(id) ?? throw new InvalidOperationException($"No key found with the id '{id}'.");
            _repo.DeleteAsync(id);

            return Task.CompletedTask;
        }

        public Task DeleteAllAsync() => _repo.DeleteAllAsync();
    }
}