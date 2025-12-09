
using CompanyInventory.Web.Data;
using CompanyInventory.Web.Models;
using CompanyInventory.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyInventory.Web.Controllers;

public class LoansController : Controller
{
    private readonly AppDbContext _db;
    private readonly IInventoryService _svc;

    public LoansController(AppDbContext db, IInventoryService svc)
    {
        _db = db; _svc = svc;
    }

    public async Task<IActionResult> Index()
        => View(await _db.Loans.Include(l => l.Item).OrderByDescending(l => l.BorrowedAt).ToListAsync());

    public async Task<IActionResult> Borrow()
    {
        ViewBag.Items = await _db.Items.Where(i => !i.IsConsumable).ToListAsync();
        return View(new Loan());
    }

    [HttpPost]
    public async Task<IActionResult> Borrow(int itemId, int quantity, string borrower, DateTime? dueAt)
    {
        try
        {
            await _svc.BorrowItemAsync(itemId, quantity, borrower, dueAt);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.Items = await _db.Items.Where(i => !i.IsConsumable).ToListAsync();
            return View(new Loan { ItemId = itemId, Quantity = quantity, Borrower = borrower, DueAt = dueAt });
        }
    }

    public async Task<IActionResult> Return(int id)
    {
        var loan = await _db.Loans.Include(l => l.Item).FirstOrDefaultAsync(l => l.Id == id);
        return loan is null ? NotFound() : View(loan);
    }

    [HttpPost, ActionName("Return")]
    public async Task<IActionResult> ReturnConfirmed(int id)
    {
        try
        {
            await _svc.ReturnLoanAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            var loan = await _db.Loans.Include(l => l.Item).FirstOrDefaultAsync(l => l.Id == id);
            return View(loan);
        }
    }
}