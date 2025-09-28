using Vanfist.Entities;
using Vanfist.Repositories;

namespace Vanfist.Services.Impl;

public class AttachmentService : IAttachmentService
{
    private static readonly HashSet<string> Allowed = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg", "image/png", "image/webp", "application/pdf"
    };

    private readonly IAttachmentRepository _repo;
    private readonly IFileStorage _storage;

    public AttachmentService(IAttachmentRepository repo, IFileStorage storage)
    {
        _repo = repo;
        _storage = storage;
    }

    public async Task<Attachment> UploadToModel(int modelId, IFormFile file, string? title, string? altText)
    {
        if (file == null || file.Length == 0) throw new InvalidOperationException("File rỗng");
        var ct = file.ContentType ?? "";
        if (!Allowed.Contains(ct)) throw new InvalidOperationException("Định dạng không được phép");

        using var s = file.OpenReadStream();
        var (savedName, relPath) = await _storage.SaveAsync(s, file.FileName, ct);

        var att = new Attachment
        {
            ModelId = modelId,
            FileName = savedName,
            Type = ct.StartsWith("image") ? "image" : "file",
            OriginalName = file.FileName,
            ContentType = ct,
            Size = file.Length,
            FilePath = relPath,
            Title = title,
            AltText = altText,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.Add(att);
        await _repo.SaveChanges();
        return att;
    }

    public Task<List<Attachment>> ListByModel(int modelId) => _repo.ListByModel(modelId);

    public async Task Delete(int id)
    {
        var a = await _repo.Get(id) ?? throw new KeyNotFoundException("Không tìm thấy file");
        await _storage.DeleteAsync(a.FilePath);
        await _repo.Delete(a);
        await _repo.SaveChanges();
    }
}
