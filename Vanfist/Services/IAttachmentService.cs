using Microsoft.AspNetCore.Http;
using Vanfist.Entities;

namespace Vanfist.Services;

public interface IAttachmentService
{
    Task<Attachment> UploadToModel(int modelId, IFormFile file, string? title, string? altText);
    Task<List<Attachment>> ListByModel(int modelId);
    Task Delete(int id);
}
