using Microsoft.EntityFrameworkCore;
using MyBoardsApp.Entities;
using MyBoardsApp.Entities.ViewModels;
using MyBoardsApp.Entities.WorkItemTypes;
using Task = MyBoardsApp.Entities.WorkItemTypes.Task;

namespace MyBoardsApp.DatabaseContext;

public class MyBoardsContext : DbContext
{
	public DbSet<WorkItem> WorkItems { get; set; }
	public DbSet<Issue> Issues { get; set; }
	public DbSet<Task> Tasks { get; set; }
	public DbSet<Epic> Epics { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Tag> Tags { get; set; }
	public DbSet<Comment> Comments { get; set; }
	public DbSet<Address> Addresses { get; set; }
	public DbSet<WorkItemState> WorkItemStates { get; set; }
	public DbSet<WorkItemTag> WorkItemTag { get; set; }
	public DbSet<TopAuthor> ViewTopAuthors { get; set; }

	public MyBoardsContext(DbContextOptions<MyBoardsContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<WorkItemState>()
			.Property(p => p.Value).IsRequired().HasMaxLength(60);

		modelBuilder.Entity<WorkItemState>()
			.HasData(
				new WorkItemState { WorkItemStateId = 1, Value = "To Do" },
				new WorkItemState { WorkItemStateId = 2, Value = "In Progress" },
				new WorkItemState { WorkItemStateId = 3, Value = "Done" });

		modelBuilder.Entity<Epic>()
			.Property(e => e.EndDate).HasPrecision(3);

		modelBuilder.Entity<Task>()
			.Property(t => t.Activity).HasMaxLength(200);

		modelBuilder.Entity<Task>()
			.Property(t => t.RemainingWork).HasPrecision(14, 2);

		modelBuilder.Entity<Issue>()
			.Property(i => i.Effort).HasColumnType("decimal(5, 2)");


		modelBuilder.Entity<WorkItem>(o =>
		{
			o.HasOne(p => p.WorkItemState)
				.WithMany()
				.HasForeignKey(w => w.WorkItemStateId);

			o.Property(p => p.IterationPath).HasColumnName("Iteration_Path");
			o.Property(p => p.Area).HasColumnType("varchar(200)");
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
						wit.HasKey(x => new { x.TagId, x.WorkItemId });
						wit.Property(x => x.PublicationDate).HasDefaultValueSql("GETUTCDATE()");
					});


		});

		modelBuilder.Entity<Comment>(o =>
		{
			o.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
			o.Property(p => p.UpdatedAt).ValueGeneratedOnUpdate();
			o.HasOne(p => p.Author)
				.WithMany(u => u.Comments)
				.HasForeignKey(c => c.AuthorId)
				.OnDelete(DeleteBehavior.ClientCascade);
		});

		modelBuilder.Entity<User>(o =>
		{
			o.HasOne(p => p.Address)
				.WithOne(u => u.User)
				.HasForeignKey<Address>(p => p.UserId);
		});

		modelBuilder.Entity<Tag>().HasData(
			new Tag { TagId = 1, Value = "Web" },
			new Tag { TagId = 2, Value = "UI" },
			new Tag { TagId = 3, Value = "Desktop" },
			new Tag { TagId = 4, Value = "API" },
			new Tag { TagId = 5, Value = "Service" });

		modelBuilder.Entity<TopAuthor>(o =>
		{
			o.ToView("View_TopAuthors");
			o.HasNoKey();
		});
	}
}