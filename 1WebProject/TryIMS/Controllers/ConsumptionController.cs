
using CompanyInventory.Web.Data;
using CompanyInventory.Web.Models;
using CompanyInventory.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyInventory.Web.Controllers;

public class ConsumptionsController : Controller
{
    private readonly AppDbContext _db;
    private readonly IInventoryService _svc;

    public ConsumptionsController(AppDbContext db, IInventoryService svc)
    {
        _db = db; _svc = svc;
    }

    public async Task<IActionResult> Index()
        => View(await _db.Consumptions.Include(c => c.Item).OrderByDescending(c => c.TakenAt).ToListAsync());

    public async Task<IActionResult> Take()
    {
        ViewBag.Items = await _db.Items.Where(i => i.IsConsumable).ToListAsync();
        return View(new Consumption());
    }

    [HttpPost]
    public async Task<IActionResult> Take(int itemId, int quantity, string takenBy, string? notes)
    {
        try
        {
            await _svc.ConsumeItemAsync(itemId, quantity, takenBy, notes);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.Items = await _db.Items.Where(i => i.IsConsumable).ToListAsync();
            return View(new Consumption { ItemId = itemId, Quantity = quantity, TakenBy = takenBy, Notes = notes });
        }
    }
}
