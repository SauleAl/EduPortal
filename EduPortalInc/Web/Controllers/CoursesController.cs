using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewBag.UserProfiles = new SelectList(_context.UserProfiles, "Id", "PhoneNumber");
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Courses courses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.UserProfiles = new SelectList(_context.UserProfiles, "Id", "PhoneNumber", courses.UserProfileId);
            return View(courses);
        }

        public async Task<IActionResult> Index()
        {
            var courses = _context.Courses.Include(c => c.UserProfile); // Подключаем UserProfile
            return View(await courses.ToListAsync());
        }
    }
}
