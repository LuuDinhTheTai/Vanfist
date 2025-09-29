using Vanfist.Entities;

namespace Vanfist.Repositories
{
    public interface IModelRepository
    {
        Task<IEnumerable<Model>> GetAllAsync();

        Task<(IEnumerable<Model> Models, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<Model?> GetByIdAsync(int id);

        Task AddAsync(Model model);
        Task UpdateAsync(Model model);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
