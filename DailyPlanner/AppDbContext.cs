using Microsoft.EntityFrameworkCore;
using DailyPlanner.Models;

namespace DailyPlanner;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<TaskItem>().ToTable("tasks");

        base.OnModelCreating(modelBuilder);
    }
}
