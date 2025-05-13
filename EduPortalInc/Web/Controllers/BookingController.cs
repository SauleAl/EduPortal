using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int coursesId)
        {
            var course = await _context.Courses.FindAsync(coursesId);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.Course = course;
            return View(new Booking { CoursesId = coursesId, BookingDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.UserId = User.Identity.Name ?? throw new InvalidOperationException("User not authenticated");
                _context.Add(booking);
                await _context.SaveChangesAsync(CancellationToken.None);
                return RedirectToAction(nameof(Confirm), new { id = booking.Id });
            }
            var course = await _context.Courses.FindAsync(booking.CoursesId);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.Course = course;
            return View(booking);
        }

        [HttpGet]
        public async Task<IActionResult> Confirm(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Courses)
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == User.Identity.Name);
            if (booking == null || booking.Courses == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(int id, string action)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null || booking.UserId != User.Identity.Name)
            {
                return NotFound();
            }
            if (action == "Confirm")
            {
                booking.Status = "Confirmed";
                _context.Update(booking);
                await _context.SaveChangesAsync(CancellationToken.None);
                return RedirectToAction(nameof(Success), new { id = booking.Id });
            }
            return RedirectToAction(nameof(Create), new { coursesId = booking.CoursesId });
        }

        [HttpGet]
        public async Task<IActionResult> Success(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Courses)
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == User.Identity.Name);
            if (booking == null || booking.Courses == null)
            {
                return NotFound();
            }
            return View(booking);
        }
    }
}