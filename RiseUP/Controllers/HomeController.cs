using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiseUp.Data;
using RiseUp.Models;

namespace RiseUp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string category = "All")
        {
            // Query base for active ideas
            var ideasQuery = _context.StartupIdeas.Include(i => i.Founder).AsQueryable();

            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                ideasQuery = ideasQuery.Where(i => i.Category == category);
            }

            // Populate dashboard data
            var model = new LandingDashboardViewModel
            {
                FeaturedIdeas = await ideasQuery.Where(i => i.IsFeatured).OrderByDescending(i => i.CreatedAt).Take(3).ToListAsync(),
                RecentIdeas = await ideasQuery.OrderByDescending(i => i.CreatedAt).Take(6).ToListAsync(),
                TotalIdeasCount = await _context.StartupIdeas.CountAsync(),
                TotalFoundersCount = await _context.Users.CountAsync(u => u.RoleType == UserType.Founder),
                TotalInvestorsCount = await _context.Users.CountAsync(u => u.RoleType == UserType.Investor),
                TotalMentorsCount = await _context.Users.CountAsync(u => u.RoleType == UserType.Mentor),
                SelectedCategory = category
            };

            return View(model);
        }
    }
}