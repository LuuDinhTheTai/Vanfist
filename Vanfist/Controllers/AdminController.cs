using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanfist.Configuration.Database;
using Vanfist.Entities;

namespace Vanfist.Controllers
{
    [Authorize(Roles = Constants.Role.Admin)]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] List<int> categories, int page = 1, int pageSize = 10)
        {
            var query = _context.Models
                .Include(m => m.Category)
                .AsQueryable();

            if (categories != null && categories.Any())
            {
                query = query.Where(m => categories.Contains(m.CategoryId ?? 0));
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.SelectedCategories = categories;

            return View(items);
        }


        //Create - GET
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList(); // load category cho dropdown
            return View();
        }

        //Create - POST
        [HttpPost]
        public async Task<IActionResult> Create(Model model, IFormFile? imageFile)
        {
            // Kiểm tra upload ảnh
            if (imageFile == null || imageFile.Length == 0)
            {
                ModelState.AddModelError("ImageError", "Bạn phải upload ảnh cho sản phẩm.");
            }

            if (ModelState.IsValid)
            {
                _context.Models.Add(model);
                await _context.SaveChangesAsync();

                // save image file
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile!.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                var attachment = new Attachment
                {
                    FileName = fileName,
                    Type = imageFile.ContentType,
                    ModelId = model.Id
                };

                _context.Attachments.Add(attachment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(model);
        }

        //Edit - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Models
                .Include(m => m.Attachments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (model == null)
                return NotFound();

            ViewBag.Categories = _context.Categories.ToList();
            return View(model);
        }

        //Edit - POST
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Vanfist.Entities.Model model, IFormFile? imageFile, bool removeImage = false)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existing = await _context.Models
                    .Include(m => m.Attachments)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (existing == null) return NotFound();

                // must have image if removing current one
                if (removeImage && (imageFile == null || imageFile.Length == 0))
                {
                    ModelState.AddModelError("ImageFile", "Phải upload ảnh mới nếu muốn xóa ảnh hiện tại.");
                    ViewBag.Categories = _context.Categories.ToList();
                    return View(model);
                }

                // Update fields
                existing.Name = model.Name;
                existing.Price = model.Price;
                existing.Length = model.Length;
                existing.Width = model.Width;
                existing.Height = model.Height;
                existing.Wheelbase = model.Wheelbase;
                existing.NEDC = model.NEDC;
                existing.MaximumPower = model.MaximumPower;
                existing.MaximumTorque = model.MaximumTorque;
                existing.RimSize = model.RimSize;
                existing.Color = model.Color;
                existing.CategoryId = model.CategoryId;

                // Helper: xoa file + record attachment
                void DeleteAttachmentFilesAndRecords()
                {
                    if (existing.Attachments != null && existing.Attachments.Any())
                    {
                        foreach (var att in existing.Attachments.ToList())
                        {
                            var oldPath = Path.Combine(_environment.WebRootPath, "uploads", att.FileName);
                            if (System.IO.File.Exists(oldPath))
                                System.IO.File.Delete(oldPath);

                            _context.Attachments.Remove(att);
                        }
                    }
                }

                // Neu xoa anh cu hoac upload anh moi
                if (removeImage || (imageFile != null && imageFile.Length > 0))
                {
                    DeleteAttachmentFilesAndRecords();
                }

                // upload new image
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var safeFileName = Path.GetFileName(imageFile.FileName);
                    var fileName = $"{Guid.NewGuid()}_{safeFileName}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    var attachment = new Attachment
                    {
                        FileName = fileName,
                        Type = imageFile.ContentType,
                        ModelId = existing.Id
                    };

                    _context.Attachments.Add(attachment);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(model);
        }

        //Delete - GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.Models
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (model == null)
                return NotFound();

            return View(model);
        }

        //Delete - POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.Models
                .Include(m => m.Attachments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (model == null)
                return NotFound();

            if (model.Attachments != null)
            {
                foreach (var attachment in model.Attachments.ToList())
                {
                    var filePath = Path.Combine(_environment.WebRootPath, "uploads", attachment.FileName);
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);

                    _context.Attachments.Remove(attachment);
                }
            }

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
