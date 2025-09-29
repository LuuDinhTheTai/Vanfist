using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanfist.DTOs.Requests;
using Vanfist.Entities;
using Vanfist.Repositories;
using Vanfist.Repositories.Impl;
using Vanfist.Services;

namespace Vanfist.Controllers
{
    [Authorize(Roles = Constants.Role.Admin)]
    public class AdminController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IModelService _modelService;
        private readonly IModelRepository _modelRepository;
        private readonly ICategoryRepository _categoryRepository;


        public AdminController(IAccountRepository accountRepository, IModelService modelService, IModelRepository modelRepository, ICategoryRepository categoryRepository)
        {
            _accountRepository = accountRepository;
            _modelService = modelService;
            _modelRepository = modelRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var admin = await _accountRepository.FindByEmail("admin@vanfist.com");
            if (admin == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Admin = admin;
            return View();
        }

        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Products(int page = 1, List<int>? categories = null, int pageSize = 10)
        {
            var (models, totalCount) = await _modelRepository.GetPagedAsync(page, pageSize);

            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return PartialView("_ProductsPartial", models);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryRepository.GetAllAsync(); // ✅
            return View(new ModelRequest());
        }


        [HttpPost]
        public async Task<IActionResult> Create(ModelRequest request, IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Phải chọn ảnh sản phẩm");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryRepository.GetAllAsync();
                return View(request);
            }

            await _modelService.AddAsync(request, imageFile);
            return RedirectToAction(nameof(Index));
        }


        //public async Task<IActionResult> Edit(int id)
        //{
        //    var model = await _modelService.GetByIdAsync(id);
        //    if (model == null) return NotFound();

        //    var request = new ModelRequest
        //    {
        //        Name = model.Name,
        //        Price = model.Price,
        //        Length = model.Length,
        //        Width = model.Width,
        //        Height = model.Height,
        //        Wheelbase = model.Wheelbase,
        //        NEDC = model.NEDC,
        //        MaximumPower = model.MaximumPower,
        //        MaximumTorque = model.MaximumTorque,
        //        RimSize = model.RimSize,
        //        Color = model.Color,
        //        CategoryId = 0
        //    };

        //    return View(request);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, ModelRequest request)
        //{
        //    if (!ModelState.IsValid) return View(request);

        //    await _modelService.UpdateAsync(id, request);
        //    return RedirectToAction(nameof(Index));
        //}

        //public async Task<IActionResult> Delete(int id)
        //{
        //    var model = await _modelService.GetByIdAsync(id);
        //    if (model == null) return NotFound();

        //    return View(model);
        //}

        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _modelService.DeleteAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
