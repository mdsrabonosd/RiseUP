using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseUp.Data;
using RiseUp.Models;

namespace RiseUp.Controllers
{
    public class StartupController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StartupController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Startup/Create (Only accessible by authenticated Founders)
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Startup/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StartupIdea model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            // Set the logged-in user as Founder
            model.FounderId = user.Id;
            model.CreatedAt = DateTime.UtcNow;

            // Remove Founder validation as it will be mapped via FounderId
            ModelState.Remove("Founder");
            ModelState.Remove("FounderId");

            if (ModelState.IsValid)
            {
                _context.StartupIdeas.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // GET: /Startup/Details/5 (View details of a specific pitch)
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var idea = await _context.StartupIdeas
                .Include(i => i.Founder)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (idea == null)
            {
                return NotFound();
            }

            // Increment views counter
            idea.ViewsCount++;
            await _context.SaveChangesAsync();

            return View(idea);
        }
    }
}