
using System.ComponentModel.DataAnnotations;

namespace CompanyInventory.Web.Models;

public enum RequestStatus { Pending, Approved, Rejected }

public class ItemRequest
{
    public int Id { get; set; }

    /// <summary>If requesting an existing catalog item, set ItemId; otherwise use Description.</summary>
    public int? ItemId { get; set; }
    public Item? Item { get; set; }

    [MaxLength(150)]
    public string? Description { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required, MaxLength(100)]
    public string RequestedBy { get; set; } = string.Empty;

    public RequestStatus Status { get; set; } = RequestStatus.Pending;

    [MaxLength(100)]
    public string? ApprovedBy { get; set; }

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DecisionAt { get; set; }
}
