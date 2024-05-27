using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardsApp.Entities;

namespace MyBoardsApp.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{

	public void Configure(EntityTypeBuilder<Comment> builder)
	{
		builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
		builder.Property(p => p.UpdatedAt).ValueGeneratedOnUpdate();
		builder.HasOne(p => p.Author)
			.WithMany(u => u.Comments)
			.HasForeignKey(c => c.AuthorId)
			.OnDelete(DeleteBehavior.ClientCascade);
	}
}