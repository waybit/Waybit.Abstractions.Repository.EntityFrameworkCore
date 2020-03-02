using System;
using Microsoft.EntityFrameworkCore;
using Waybit.Abstractions.Domain;
using Waybit.Abstractions.Repository.EntityFrameworkCore.IntegrationTests.Fakes;

namespace Waybit.Abstractions.Repository.EntityFrameworkCore.IntegrationTests.Testing
{
	public class RepositoryScope : IDisposable
	{
		private readonly DbContext _dbContext;

		public RepositoryScope(DbContext dbContext)
		{
			_dbContext = dbContext 
				?? throw new ArgumentNullException(nameof(dbContext));
			
			Repository = new FakeRepository(_dbContext);
		}

		public IRepository<FakeAggregateRoot, int> Repository { get; private set; }

		/// <inheritdoc />
		public void Dispose()
		{
			_dbContext.SaveChanges();
		}
	}
}
