using Microsoft.EntityFrameworkCore;
using Vanfist.Configuration.Database;
using Vanfist.Entities;

namespace Vanfist.Repositories.Impl;

public class AttachmentRepository : IAttachmentRepository
{
    private readonly ApplicationDbContext _db;
    public AttachmentRepository(ApplicationDbContext db) => _db = db;

    public Task<List<Attachment>> ListByModel(int modelId) =>
        _db.Attachments
           .Where(x => x.ModelId == modelId)
           .OrderBy(x => x.SortOrder).ThenByDescending(x => x.Id)
           .ToListAsync();

    public Task<Attachment?> Get(int id) =>
        _db.Attachments.FirstOrDefaultAsync(x => x.Id == id);

    public async Task Add(Attachment a) => await _db.Attachments.AddAsync(a);

    public Task Delete(Attachment a)
    {
        _db.Attachments.Remove(a);
        return Task.CompletedTask;
    }

    public Task SaveChanges() => _db.SaveChangesAsync();
}
