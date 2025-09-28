using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Vanfist.Configuration.Database;
using Vanfist.Entities;
using Vanfist.Services;
using Vanfist.Models;

namespace Vanfist.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ApplicationDbContext _context;

        public HomeController(
            IAccountService accountService,
            ApplicationDbContext context)
        {
            _accountService = accountService;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 12, [FromQuery] List<int> categories = null)
        {
            var query = _context.Models
                .Include(m => m.Category)
                .Include(m => m.Attachments)
                .AsQueryable();

            // filter by categoriyId
            if (categories != null && categories.Any())
            {
                query = query.Where(m => m.CategoryId.HasValue && categories.Contains(m.CategoryId.Value));
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.SelectedCategories = categories ?? new List<int>();

            return View(items);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = _context.Models
                .Include(m => m.Category)
                .Include(m => m.Attachments)
                .FirstOrDefault(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
