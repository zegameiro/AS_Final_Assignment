using Piranha.Models;
using Microsoft.EntityFrameworkCore;

namespace Piranha.Repositories
{
    internal class KeyRepository : IKeyRepository
    {
        private readonly IDb _db;

        public KeyRepository(IDb db)
        {
            _db = db;
        }

        public Task DeleteAllAsync()
        {
            return _db.Keys.ExecuteDeleteAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            return _db.Keys
                .Where(k => k.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Key>> GetAllAsync()
        {
            return await _db.Keys
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Key> GetByIdAsync(Guid id)
        {
            return await _db.Keys
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<Key> GetByNameAsync(string name)
        {
            return await _db.Keys
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.Name == name);
        }

        public async Task<Key> SaveAsync(Key key)
        {
            var isNew = key.Id == Guid.Empty;
            if (isNew)
            {
                key.Id = Guid.NewGuid();
                await _db.Keys.AddAsync(key);
            }
            else
            {
                var dbKey = await _db.Keys.FirstOrDefaultAsync(k => k.Id == key.Id);
                if (dbKey != null)
                {
                    dbKey.Name = key.Name;
                }
                else
                {
                    // Instead of throwing, create a new key with the given Id and Name
                    await _db.Keys.AddAsync(key);
                }
            }

            await _db.SaveChangesAsync();
            return key;
        }
    }
}