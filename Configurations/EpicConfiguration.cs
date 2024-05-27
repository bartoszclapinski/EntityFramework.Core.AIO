using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardsApp.Entities.WorkItemTypes;

namespace MyBoardsApp.Configurations;

public class EpicConfiguration : IEntityTypeConfiguration<Epic>
{

	public void Configure(EntityTypeBuilder<Epic> builder)
	{
		builder.Property(e => e.EndDate).HasPrecision(3);
	}
}