using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardsApp.Entities;

namespace MyBoardsApp.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{

	public void Configure(EntityTypeBuilder<Address> builder)
	{
		builder.OwnsOne(a => a.Coordinate,
			o =>
			{
				o.Property(c => c.Latitude).HasPrecision(18, 7);
				o.Property(c => c.Longitude).HasPrecision(18, 7);
			});
	}
}