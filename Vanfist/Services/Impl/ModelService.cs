using Vanfist.DTOs.Responses;
using Vanfist.Repositories;
using Vanfist.DTOs.Requests;
using Vanfist.Entities;

namespace Vanfist.Services.Impl
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;
        private readonly IWebHostEnvironment _env;

        public ModelService(IModelRepository modelRepository, IWebHostEnvironment env)
        {
            _modelRepository = modelRepository;
            _env = env;
        }

        public async Task<(IEnumerable<ModelResponse> Models, int TotalCount)> GetPagedModelsAsync(int page, int pageSize)
        {
            var (models, totalCount) = await _modelRepository.GetPagedAsync(page, pageSize);

            var result = models.Select(m => new ModelResponse
            {
                Id = m.Id,
                Name = m.Name,
                Price = m.Price,
                Attachments = m.Attachments.Select(a => a.FileName).ToList()
            }).ToList();

            return (result, totalCount);
        }
        public async Task<ModelResponse?> GetByIdAsync(int id)
        {
            var m = await _modelRepository.GetByIdAsync(id);
            if (m == null) return null;

            return new ModelResponse
            {
                Id = m.Id,
                Name = m.Name,
                Price = m.Price,
                Length = m.Length,
                Width = m.Width,
                Height = m.Height,
                Wheelbase = m.Wheelbase,
                NEDC = m.NEDC,
                MaximumPower = m.MaximumPower,
                MaximumTorque = m.MaximumTorque,
                RimSize = m.RimSize,
                Color = m.Color.ToString(),
                CategoryName = m.Category?.Name ?? "N/A",
                Attachments = m.Attachments.Select(a => a.FileName).ToList()
            };
        }

        public async Task AddAsync(ModelRequest request, IFormFile? imageFile)
        {
            string? fileName = null;

            if (imageFile != null && imageFile.Length > 0)
            {
                fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var savePath = Path.Combine(_env.WebRootPath, "images/products", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }

            var model = new Model
            {
                Name = request.Name,
                Price = request.Price,
                Length = request.Length,
                Width = request.Width,
                Height = request.Height,
                Wheelbase = request.Wheelbase,
                NEDC = request.NEDC,
                MaximumPower = request.MaximumPower,
                MaximumTorque = request.MaximumTorque,
                RimSize = request.RimSize,
                Color = request.Color,
                CategoryId = request.CategoryId
            };

            if (!string.IsNullOrEmpty(fileName))
            {
                model.Attachments.Add(new Attachment
                {
                    FileName = fileName,   // ✅ chỉ lưu tên file
                    Type = "image"         // nhớ set Type vì db yêu cầu
                });
            }

            await _modelRepository.AddAsync(model);
            await _modelRepository.SaveChangesAsync();
        }



        public async Task UpdateAsync(int id, ModelRequest request)
        {
            var model = await _modelRepository.GetByIdAsync(id);
            if (model == null) return;

            model.Name = request.Name;
            model.Price = request.Price;
            model.Length = request.Length;
            model.Width = request.Width;
            model.Height = request.Height;
            model.Wheelbase = request.Wheelbase;
            model.NEDC = request.NEDC;
            model.MaximumPower = request.MaximumPower;
            model.MaximumTorque = request.MaximumTorque;
            model.RimSize = request.RimSize;
            model.Color = request.Color;
            model.CategoryId = request.CategoryId;

            await _modelRepository.UpdateAsync(model);
            await _modelRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _modelRepository.DeleteAsync(id);
            await _modelRepository.SaveChangesAsync();
        }

    }
}
