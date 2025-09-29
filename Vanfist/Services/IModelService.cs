using Vanfist.DTOs.Responses;
using Vanfist.DTOs.Requests;

namespace Vanfist.Services
{
    public interface IModelService
    {
        Task<(IEnumerable<ModelResponse> Models, int TotalCount)> GetPagedModelsAsync(int page, int pageSize);
        Task<ModelResponse?> GetByIdAsync(int id);

        Task AddAsync(ModelRequest request, IFormFile? imageFile);
        Task UpdateAsync(int id, ModelRequest request);
        Task DeleteAsync(int id);
    }
}
