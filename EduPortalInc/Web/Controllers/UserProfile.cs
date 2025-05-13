using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Entities;
using System.Threading.Tasks;

public class UserProfileController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserProfileController(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IActionResult> Index()
    {
        var userProfiles = await _context.UserProfiles.ToListAsync();
        return View(userProfiles);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserProfile userProfile)
    {
        if (!ModelState.IsValid)
        {
            return View(userProfile);
        }

        try
        {
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError("", "Ошибка при сохранении данных. Проверьте вводимые значения.");
            return View(userProfile);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var userProfile = await _context.UserProfiles.FindAsync(id);
        if (userProfile == null)
        {
            return NotFound();
        }
        return View(userProfile);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, UserProfile userProfile)
    {
        if (id != userProfile.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(userProfile);
        }

        try
        {
            _context.Update(userProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await UserProfileExists(userProfile.Id))
            {
                return NotFound();
            }
            throw;
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError("", "Ошибка при обновлении данных. Проверьте вводимые значения.");
            return View(userProfile);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var userProfile = await _context.UserProfiles.FindAsync(id);
        if (userProfile == null)
        {
            return NotFound();
        }
        return View(userProfile);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userProfile = await _context.UserProfiles.FindAsync(id);
        if (userProfile != null)
        {
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var userProfile = await _context.UserProfiles.FindAsync(id);
        if (userProfile == null)
        {
            return NotFound();
        }
        return View(userProfile);
    }

    private async Task<bool> UserProfileExists(int id)
    {
        return await _context.UserProfiles.AnyAsync(e => e.Id == id);
    }
}