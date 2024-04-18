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
}