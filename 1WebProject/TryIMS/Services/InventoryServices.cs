
using CompanyInventory.Web.Data;
using CompanyInventory.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyInventory.Web.Services;

public interface IInventoryService
{
    Task<Loan> BorrowItemAsync(int itemId, int qty, string borrower, DateTime? dueAt);
    Task<Loan> ReturnLoanAsync(int loanId);
    Task<Consumption> ConsumeItemAsync(int itemId, int qty, string takenBy, string? notes);
    Task<ItemRequest> CreateRequestAsync(int? itemId, string? description, int qty, string requestedBy);
    Task<ItemRequest> ApproveRequestAsync(int requestId, string approvedBy);
    Task<ItemRequest> RejectRequestAsync(int requestId, string approvedBy);
}

public class InventoryService : IInventoryService
{
    private readonly AppDbContext _db;

    public InventoryService(AppDbContext db) => _db = db;

    public async Task<Loan> BorrowItemAsync(int itemId, int qty, string borrower, DateTime? dueAt)
    {
        var item = await _db.Items.FirstOrDefaultAsync(i => i.Id == itemId)
                   ?? throw new InvalidOperationException("Item not found.");
        if (item.IsConsumable) throw new InvalidOperationException("Consumables cannot be borrowed.");
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        if (qty > item.QuantityOnHand) throw new InvalidOperationException("Insufficient stock to borrow.");

        item.QuantityOnHand -= qty;

        var loan = new Loan
        {
            ItemId = itemId,
            Quantity = qty,
            Borrower = borrower,
            DueAt = dueAt,
            Status = LoanStatus.Borrowed
        };

        _db.Loans.Add(loan);
        await _db.SaveChangesAsync();
        return loan;
    }

    public async Task<Loan> ReturnLoanAsync(int loanId)
    {
        var loan = await _db.Loans.Include(l => l.Item).FirstOrDefaultAsync(l => l.Id == loanId)
                   ?? throw new InvalidOperationException("Loan not found.");
        if (loan.Status == LoanStatus.Returned) throw new InvalidOperationException("Loan already returned.");

        loan.Status = LoanStatus.Returned;
        loan.ReturnedAt = DateTime.UtcNow;

        loan.Item!.QuantityOnHand += loan.Quantity;

        await _db.SaveChangesAsync();
        return loan;
    }

    public async Task<Consumption> ConsumeItemAsync(int itemId, int qty, string takenBy, string? notes)
    {
        var item = await _db.Items.FirstOrDefaultAsync(i => i.Id == itemId)
                   ?? throw new InvalidOperationException("Item not found.");
        if (!item.IsConsumable) throw new InvalidOperationException("Non-consumables cannot be consumed.");
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        if (qty > item.QuantityOnHand) throw new InvalidOperationException("Insufficient stock to consume.");

        item.QuantityOnHand -= qty;

        var cons = new Consumption
        {
            ItemId = itemId,
            Quantity = qty,
            TakenBy = takenBy,
            Notes = notes
        };
        _db.Consumptions.Add(cons);

        await _db.SaveChangesAsync();
        return cons;
    }

    public async Task<ItemRequest> CreateRequestAsync(int? itemId, string? description, int qty, string requestedBy)
    {
        if (itemId is null && string.IsNullOrWhiteSpace(description))
            throw new InvalidOperationException("Provide ItemId or Description.");

        var req = new ItemRequest
        {
            ItemId = itemId,
            Description = description,
            Quantity = qty,
            RequestedBy = requestedBy,
            Status = RequestStatus.Pending
        };
        _db.ItemRequests.Add(req);
        await _db.SaveChangesAsync();
        return req;
    }

    public async Task<ItemRequest> ApproveRequestAsync(int requestId, string approvedBy)
    {
        var req = await _db.ItemRequests.Include(r => r.Item).FirstOrDefaultAsync(r => r.Id == requestId)
                  ?? throw new InvalidOperationException("Request not found.");
        if (req.Status != RequestStatus.Pending) throw new InvalidOperationException("Request is not pending.");

        // Optional: if the request is for an existing item, reserve or deduct immediately.
        if (req.ItemId is not null && req.Item is not null)
        {
            if (req.Item.IsConsumable)
            {
                if (req.Item.QuantityOnHand < req.Quantity)
                    throw new InvalidOperationException("Insufficient stock to approve.");
                req.Item.QuantityOnHand -= req.Quantity;
            }
            else
            {
                if (req.Item.QuantityOnHand < req.Quantity)
                    throw new InvalidOperationException("Insufficient stock to approve.");
                req.Item.QuantityOnHand -= req.Quantity; // treat as allocated for request
            }
        }

        req.Status = RequestStatus.Approved;
        req.ApprovedBy = approvedBy;
        req.DecisionAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return req;
    }

    public async Task<ItemRequest> RejectRequestAsync(int requestId, string approvedBy)
    {
        var req = await _db.ItemRequests.FirstOrDefaultAsync(r => r.Id == requestId)
                  ?? throw new InvalidOperationException("Request not found.");
        if (req.Status != RequestStatus.Pending) throw new InvalidOperationException("Request is not pending.");

        req.Status = RequestStatus.Rejected;
        req.ApprovedBy = approvedBy;
        req.DecisionAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return req;
    }
}