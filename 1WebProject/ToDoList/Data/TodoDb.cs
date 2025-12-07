using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data;
public class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options)
        : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>().ToTable("Todos");
        
        modelBuilder.Entity<Todo>()
        .Property(t=>t.Title)
        .IsRequired().HasMaxLength(200);

        modelBuilder.Entity<Todo>()
        .HasIndex(t=>t.IsCompleted);
    }
}