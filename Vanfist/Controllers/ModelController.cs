// Controllers/ModelController.cs
using Microsoft.AspNetCore.Mvc;
using Vanfist.Repositories;
using Vanfist.Services;

namespace Vanfist.Controllers
{
    public class ModelController : Controller
    {
        private readonly IModelRepository _modelRepo;
        private readonly IAttachmentService _attSvc;

        public ModelController(IModelRepository modelRepo, IAttachmentService attSvc)
        {
            _modelRepo = modelRepo;
            _attSvc = attSvc;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await _modelRepo.ListAll(); 
            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _modelRepo.GetById(id);
            if (model == null) return NotFound();

            var files = await _attSvc.ListByModel(id);
            var vm = new Vanfist.ViewModels.ModelDetailViewModel
            {
                Model = model,
                Attachments = files
            };
            return View(vm);
        }
    }
}
