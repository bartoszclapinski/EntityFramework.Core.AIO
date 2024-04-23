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
            //   
            o.Property(p => p.State).IsRequired();
            o.Property(p => p.IterationPath).HasColumnName("Iteration_Path");
            o.Property(p => p.Area).HasColumnType("varchar(200)");
            o.Property(p => p.Effort).HasColumnType("decimal(5, 2)");
            o.Property(p => p.EndDate).HasPrecision(3);
            o.Property(p => p.RemainingWork).HasPrecision(14, 2);
            o.Property(p => p.Activity).HasPrecision(14, 2);
            o.HasMany(p => p.Comments)
                            .WithOne(c => c.WorkItem)
                            .HasForeignKey(c => c.WorkItemId);
	        
            o.HasOne(p => p.Author)
                            .WithMany(u => u.WorkItems)
                            .HasForeignKey(w => w.AuthorId);

            o.HasMany(p => p.Tags)
                            .WithMany(t => t.WorkItems)
                            .UsingEntity<WorkItemTag>(
                                            w => w.HasOne(wit => wit.Tag)
                                                            .WithMany()
                                                            .HasForeignKey(wit => wit.TagId),
                                            w => w.HasOne(wit => wit.WorkItem)
                                                            .WithMany()
                                                            .HasForeignKey(wit => wit.WorkItemId),
                                            wit =>
                                            {
	                                            wit.HasKey(x => new {x.TagId, x.WorkItemId});
	                                            wit.Property(x => x.PublicationDate).HasDefaultValueSql("GETUTCDATE()");
                                            });


        });

        modelBuilder.Entity<Comment>(o =>
        {
            o.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            o.Property(p => p.UpdatedAt).ValueGeneratedOnUpdate();
        });
        
        modelBuilder.Entity<User>(o =>
        {
            o.HasOne(p => p.Address)
                .WithOne(u => u.User)
                .HasForeignKey<Address>(p => p.UserId);
        });

        
        
        
    }
}