using Microsoft.EntityFrameworkCore;
using Vanfist.Configuration.Database;
using Vanfist.Entities;

namespace Vanfist.Repositories.Impl
{
    public class ModelRepository : IModelRepository
    {
        private readonly ApplicationDbContext _db;
        public ModelRepository(ApplicationDbContext db) => _db = db;

        public Task<Model?> GetById(int id) =>
            _db.Models.Include(m => m.Category).FirstOrDefaultAsync(m => m.Id == id);

        public Task<List<Model>> ListAll() =>                  
            _db.Models.Include(m => m.Category)
                      .OrderBy(m => m.Name)
                      .ToListAsync();
    }
}
