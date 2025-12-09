
using System.ComponentModel.DataAnnotations;

namespace CompanyInventory.Web.Models;

public enum LoanStatus { Borrowed, Returned }

public class Loan
{
    public int Id { get; set; }

    [Required]
    public int ItemId { get; set; }
    public Item? Item { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required, MaxLength(100)]
    public string Borrower { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime BorrowedAt { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Date)]
    public DateTime? DueAt { get; set; }

    public DateTime? ReturnedAt { get; set; }

    public LoanStatus Status { get; set; } = LoanStatus.Borrowed;
}