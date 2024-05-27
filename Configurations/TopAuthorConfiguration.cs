using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardsApp.Entities.ViewModels;

namespace MyBoardsApp.Configurations;

public class TopAuthorConfiguration : IEntityTypeConfiguration<TopAuthor>
{

	public void Configure(EntityTypeBuilder<TopAuthor> builder)
	{
		builder.ToView("View_TopAuthors");
		builder.HasNoKey();
	}
}