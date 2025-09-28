using System.Threading.Tasks;
using Vanfist.Entities;

namespace Vanfist.Repositories
{
    public interface IModelRepository
    {
        Task<Model?> GetById(int id);
        Task<List<Model>> ListAll();
    }
}
