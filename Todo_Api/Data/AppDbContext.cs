using Microsoft.EntityFrameworkCore;
using Todo_Api.Configuration;
using Todo_Api.Models;

namespace Todo_Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TodoTask> Todos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new MissionEntityType().Configure(modelBuilder.Entity<TodoTask>());
    }


}
