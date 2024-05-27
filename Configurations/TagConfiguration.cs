using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardsApp.Entities;

namespace MyBoardsApp.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{

	public void Configure(EntityTypeBuilder<Tag> builder)
	{
		builder.HasData(
			new Tag { TagId = 1, Value = "Web" },
			new Tag { TagId = 2, Value = "UI" },
			new Tag { TagId = 3, Value = "Desktop" },
			new Tag { TagId = 4, Value = "API" },
			new Tag { TagId = 5, Value = "Service" });
	}
}