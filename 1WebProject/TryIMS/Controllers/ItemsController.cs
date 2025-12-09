
using CompanyInventory.Web.Data;
using CompanyInventory.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyInventory.Web.Controllers;

public class ItemsController : Controller
{
    private readonly AppDbContext _db;
    public ItemsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
        => View(await _db.Items.OrderBy(i => i.Name).ToListAsync());

    public IActionResult Create() => View(new Item());

    [HttpPost]
    public async Task<IActionResult> Create(Item item)
    {
        if (!ModelState.IsValid) return View(item);
        _db.Items.Add(item);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _db.Items.FindAsync(id);
        return item is null ? NotFound() : View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Item item)
    {
        if (!ModelState.IsValid) return View(item);
        _db.Update(item);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
