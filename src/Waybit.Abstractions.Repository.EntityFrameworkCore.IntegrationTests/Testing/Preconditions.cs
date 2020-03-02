using System;
using Microsoft.EntityFrameworkCore;
using Waybit.Abstractions.Repository.EntityFrameworkCore.IntegrationTests.Fakes;

namespace Waybit.Abstractions.Repository.EntityFrameworkCore.IntegrationTests.Testing
{
	public class Preconditions
	{
		private readonly DbContext _dbContext;

		public Preconditions(DbContext dbContext)
		{
			_dbContext = dbContext 
				?? throw new ArgumentNullException(nameof(dbContext));
		}
		
		/// <summary>
		/// Ensures that there is fake aggregate roots with the specified count
		/// </summary>
		/// <param name="count">Count of fake aggregate roots</param>
		public Preconditions EnsureFakes(int count)
		{
			for (int i = 0; i < count; i++)
			{
				var fakeAggregateRoot = new FakeAggregateRoot(GenerateRandomName());

				_dbContext
					.Set<FakeAggregateRoot>()
					.Add(fakeAggregateRoot);
			}

			_dbContext.SaveChanges();

			return this;
		}

		/// <summary>
		/// Ensures that is fake aggregate root
		/// </summary>
		public Preconditions EnsureRandomFake()
		{
			var fakeAggregateRoot = new FakeAggregateRoot(GenerateRandomName());

			_dbContext
				.Set<FakeAggregateRoot>()
				.Add(fakeAggregateRoot);

			_dbContext.SaveChanges();

			return this;
		}

		private static string GenerateRandomName()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
