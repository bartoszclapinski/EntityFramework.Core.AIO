using Microsoft.EntityFrameworkCore;
using MyBoardsApp.Configurations;
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
		//	new AddressConfiguration().Configure(modelBuilder.Entity<Address>());
		
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
			
	}
}