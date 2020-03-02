using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Waybit.Abstractions.Repository.EntityFrameworkCore.IntegrationTests.Fakes
{
	/// <summary>
	/// Entity framework mapping
	/// </summary>
	public class FakeAggregateRootEntityTypeConfiguration : IEntityTypeConfiguration<FakeAggregateRoot>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<FakeAggregateRoot> builder)
		{
			builder.HasKey(e => e.Id);
			
			builder.Property(e => e.Name)
				.IsRequired();
		}
	}
}
