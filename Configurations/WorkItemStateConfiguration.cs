using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardsApp.Entities;

namespace MyBoardsApp.Configurations;

public class WorkItemStateConfiguration : IEntityTypeConfiguration<WorkItemState>
{

	public void Configure(EntityTypeBuilder<WorkItemState> builder)
	{
		builder.HasData(
			new WorkItemState { WorkItemStateId = 1, Value = "To Do" },
			new WorkItemState { WorkItemStateId = 2, Value = "In Progress" },
			new WorkItemState { WorkItemStateId = 3, Value = "Done" });
		
		builder.Property(p => p.Value).IsRequired().HasMaxLength(60);
	}
}