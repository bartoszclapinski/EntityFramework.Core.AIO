using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardsApp.Entities.WorkItemTypes;

namespace MyBoardsApp.Configurations;

public class IssueConfiguration : IEntityTypeConfiguration<Issue>
{

	public void Configure(EntityTypeBuilder<Issue> builder)
	{
		builder.Property(i => i.Effort).HasColumnType("decimal(5, 2)");
	}
}