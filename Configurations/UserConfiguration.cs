using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardsApp.Entities;

namespace MyBoardsApp.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{

	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasOne(p => p.Address)
			.WithOne(u => u.User)
			.HasForeignKey<Address>(p => p.UserId);

		builder.HasIndex(u => new { u.Email, u.FullName })
			.HasDatabaseName("IX_Users_Email_FullName");
	}
}