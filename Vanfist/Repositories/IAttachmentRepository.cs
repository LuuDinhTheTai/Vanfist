using Vanfist.Entities;

namespace Vanfist.Repositories;

public interface IAttachmentRepository
{
    Task<List<Attachment>> ListByModel(int modelId);
    Task<Attachment?> Get(int id);
    Task Add(Attachment a);
    Task Delete(Attachment a);
    Task SaveChanges();
}
