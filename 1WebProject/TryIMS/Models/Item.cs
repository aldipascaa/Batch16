
using System.ComponentModel.DataAnnotations;

namespace CompanyInventory.Web.Models;

public class Item
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Sku { get; set; }

    /// <summary>True for consumables (e.g., paper), false for tools/equipment.</summary>
    public bool IsConsumable { get; set; }

    /// <summary>Current stock for consumables or available units for borrowable items.</summary>
    [Range(0, int.MaxValue)]
    public int QuantityOnHand { get; set; }

    /// <summary>Row version for optimistic concurrency.</summary>
    [Timestamp]
    public byte[]? RowVersion { get; set; }
}
