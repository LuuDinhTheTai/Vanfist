using Microsoft.EntityFrameworkCore;
using Vanfist.Configuration.Database;
using Vanfist.Entities;

namespace Vanfist.Repositories.Impl
{
    public class ModelRepository : IModelRepository
    {
        private readonly ApplicationDbContext _context;

        public ModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Model> Models, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Models
                .Include(m => m.Attachments)
                .AsQueryable();

            var totalCount = await query.CountAsync();
            var models = await query
                .OrderBy(m => m.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (models, totalCount);
        }


        public async Task<IEnumerable<Model>> GetAllAsync()
        {
            return await _context.Models
                .Include(m => m.Category)
                .ToListAsync();
        }


        public async Task<Model?> GetByIdAsync(int id)
        {
            return await _context.Models
                .Include(m => m.Category)
                .Include(m => m.Attachments)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Model model)
        {
            await _context.Models.AddAsync(model);
        }

        public async Task UpdateAsync(Model model)
        {
            _context.Models.Update(model);
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model != null)
                _context.Models.Remove(model);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
