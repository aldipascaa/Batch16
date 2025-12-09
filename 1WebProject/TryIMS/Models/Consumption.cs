
using System.ComponentModel.DataAnnotations;

namespace CompanyInventory.Web.Models;

public class Consumption
{
    public int Id { get; set; }

    [Required]
    public int ItemId { get; set; }
    public Item? Item { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required, MaxLength(100)]
    public string TakenBy { get; set; } = string.Empty;

    public DateTime TakenAt { get; set; } = DateTime.UtcNow;

    [MaxLength(200)]
    public string? Notes { get; set; }
}