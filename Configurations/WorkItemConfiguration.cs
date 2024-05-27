using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardsApp.Entities;

namespace MyBoardsApp.Configurations;

public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
{

	public void Configure(EntityTypeBuilder<WorkItem> builder)
	{
		builder.HasOne(p => p.WorkItemState)
			.WithMany()
			.HasForeignKey(w => w.WorkItemStateId);

		builder.Property(p => p.IterationPath).HasColumnName("Iteration_Path");
		builder.Property(p => p.Area).HasColumnType("varchar(200)");
		builder.HasMany(p => p.Comments)
			.WithOne(c => c.WorkItem)
			.HasForeignKey(c => c.WorkItemId);

		builder.HasOne(p => p.Author)
			.WithMany(u => u.WorkItems)
			.HasForeignKey(w => w.AuthorId);

		builder.HasMany(p => p.Tags)
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
	}
}