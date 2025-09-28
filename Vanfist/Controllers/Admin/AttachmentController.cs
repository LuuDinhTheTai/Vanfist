using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vanfist.Services;

namespace Vanfist.Controllers.Admin;

[Authorize(Roles = "Admin")]
public class AttachmentController : Controller
{
    private readonly IAttachmentService _svc;
    private readonly ILogger<AttachmentController> _logger;
    public AttachmentController(IAttachmentService svc, ILogger<AttachmentController> logger)
    {
        _svc = svc; _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int modelId)
    {
        var items = await _svc.ListByModel(modelId);
        ViewBag.ModelId = modelId;
        return View(items);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(int modelId, IFormFile file, string? title, string? altText)
    {
        if (file == null) { TempData["Error"] = "Chưa chọn file"; return RedirectToAction(nameof(Index), new { modelId }); }
        try
        {
            await _svc.UploadToModel(modelId, file, title, altText);
            TempData["Success"] = "Tải lên thành công";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Upload failed");
            TempData["Error"] = ex.Message;
        }
        return RedirectToAction(nameof(Index), new { modelId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, int modelId)
    {
        try
        {
            await _svc.Delete(id);
            TempData["Success"] = "Đã xoá";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete failed");
            TempData["Error"] = ex.Message;
        }
        return RedirectToAction(nameof(Index), new { modelId });
    }
}
