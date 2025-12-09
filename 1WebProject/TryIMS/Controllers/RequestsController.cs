
using CompanyInventory.Web.Data;
using CompanyInventory.Web.Models;
using CompanyInventory.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyInventory.Web.Controllers;

public class RequestsController : Controller
{
    private readonly AppDbContext _db;
    private readonly IInventoryService _svc;

    public RequestsController(AppDbContext db, IInventoryService svc)
    { _db = db; _svc = svc; }

    public async Task<IActionResult> Index()
        => View(await _db.ItemRequests.Include(r => r.Item).OrderByDescending(r => r.RequestedAt).ToListAsync());

    public async Task<IActionResult> Create()
    {
        ViewBag.Items = await _db.Items.OrderBy(i => i.Name).ToListAsync();
        return View(new ItemRequest());
    }

    [HttpPost]
    public async Task<IActionResult> Create(int? itemId, string? description, int quantity, string requestedBy)
    {
        try
        {
            await _svc.CreateRequestAsync(itemId, description, quantity, requestedBy);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.Items = await _db.Items.OrderBy(i => i.Name).ToListAsync();
            return View(new ItemRequest { ItemId = itemId, Description = description, Quantity = quantity, RequestedBy = requestedBy });
        }
    }

    public async Task<IActionResult> Approve(int id)
    {
        var req = await _db.ItemRequests.Include(r => r.Item).FirstOrDefaultAsync(r => r.Id == id);
        return req is null ? NotFound() : View(req);
    }

    [HttpPost, ActionName("Approve")]
    public async Task<IActionResult> ApproveConfirmed(int id, string approvedBy)
    {
        try
        {
            await _svc.ApproveRequestAsync(id, approvedBy);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            var req = await _db.ItemRequests.Include(r => r.Item).FirstOrDefaultAsync(r => r.Id == id);
            return View(req);
        }
    }

    public async Task<IActionResult> Reject(int id)
    {
        var req = await _db.ItemRequests.Include(r => r.Item).FirstOrDefaultAsync(r => r.Id == id);
        return req is null ? NotFound() : View(req);
    }

    [HttpPost, ActionName("Reject")]
    public async Task<IActionResult> RejectConfirmed(int id, string approvedBy)
    {
        try
        {
            await _svc.RejectRequestAsync(id, approvedBy);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            var req = await _db.ItemRequests.Include(r => r.Item).FirstOrDefaultAsync(r => r.Id == id);
            return View(req);
        }
    }
}