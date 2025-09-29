using Microsoft.AspNetCore.Mvc;
using Vanfist.Services;

namespace Vanfist.Controllers
{
    public class HomeController : Controller
    {
        private readonly IModelService _modelService;
        private const int PageSize = 12; 

        public HomeController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var (models, totalCount) = await _modelService.GetPagedModelsAsync(page, PageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _modelService.GetByIdAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }
    }
}
