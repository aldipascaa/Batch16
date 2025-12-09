
using CompanyInventory.Web.Data;
using CompanyInventory.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// SQLite connection
var connString = builder.Configuration.GetConnectionString("Default") ?? "Data Source=inventory.db";
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(connString));

builder.Services.AddControllersWithViews();

// Business service
builder.Services.AddScoped<IInventoryService, InventoryService>();

var app = builder.Build();

// Apply migrations & seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Items}/{action=Index}/{id?}");

app.Run();
