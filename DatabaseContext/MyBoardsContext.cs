using Microsoft.EntityFrameworkCore;
using MyBoardsApp.Entities;

namespace MyBoardsApp.DatabaseContext;

public class MyBoardsContext : DbContext
{
    public DbSet<WorkItem> WorkItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public MyBoardsContext(DbContextOptions<MyBoardsContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkItem>(o =>
        {
            o.Property(p => p.State).IsRequired();
            o.Property(p => p.IterationPath).HasColumnName("Iteration_Path");
            o.Property(p => p.Area).HasColumnType("varchar(200)");
            o.Property(p => p.Effort).HasColumnType("decimal(5, 2)");
            o.Property(p => p.EndDate).HasPrecision(3);
            o.Property(p => p.RemainingWork).HasPrecision(14, 2);
            o.Property(p => p.Activity).HasPrecision(14, 2);
        });

        modelBuilder.Entity<Comment>(o =>
        {
            o.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            o.Property(p => p.UpdatedAt).ValueGeneratedOnUpdate();
        });
    }
}