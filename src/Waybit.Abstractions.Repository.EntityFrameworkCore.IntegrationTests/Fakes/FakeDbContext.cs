using Microsoft.EntityFrameworkCore;

namespace Waybit.Abstractions.Repository.EntityFrameworkCore.IntegrationTests.Fakes
{
	/// <summary>
	/// Testing entity framework database context
	/// </summary>
	public class FakeDbContext : DbContext
	{
		/// <inheritdoc />
		public FakeDbContext(DbContextOptions options)
			: base(options)
		{
		}

		/// <summary>
		/// Testing database set
		/// </summary>
		public DbSet<FakeAggregateRoot> FakeAggregateRoots { get; set; }

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new FakeAggregateRootEntityTypeConfiguration());
		}
	}
}
