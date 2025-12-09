
using CompanyInventory.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyInventory.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Item> Items => Set<Item>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<Consumption> Consumptions => Set<Consumption>();
    public DbSet<ItemRequest> ItemRequests => Set<ItemRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Simple indexes
        modelBuilder.Entity<Item>()
            .HasIndex(i => i.Sku)
            .IsUnique(false);

        // Seed minimal demo data
        modelBuilder.Entity<Item>().HasData(
            new Item { Id = 1, Name = "Laptop Dell", IsConsumable = false, QuantityOnHand = 5, Sku = "EQUIP-001" },
            new Item { Id = 2, Name = "Printer Paper A4", IsConsumable = true, QuantityOnHand = 500, Sku = "CONS-001" }
        );
    }
}
