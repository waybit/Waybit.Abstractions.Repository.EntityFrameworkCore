using Microsoft.EntityFrameworkCore;

namespace Waybit.Abstractions.Repository.EntityFrameworkCore.IntegrationTests.Fakes
{
	/// <summary>
	/// Testing instance of repository
	/// </summary>
	internal class FakeRepository : Repository<FakeAggregateRoot, int>
	{
		/// <inheritdoc />
		public FakeRepository(DbContext dbContext)
			: base(dbContext)
		{
		}
	}
}
