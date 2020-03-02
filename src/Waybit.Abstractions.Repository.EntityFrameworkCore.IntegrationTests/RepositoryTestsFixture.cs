using System.Threading;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Waybit.Abstractions.Repository.EntityFrameworkCore.IntegrationTests.Comparers;
using Waybit.Abstractions.Repository.EntityFrameworkCore.IntegrationTests.Fakes;
using Waybit.Abstractions.Repository.EntityFrameworkCore.IntegrationTests.Testing;

namespace Waybit.Abstractions.Repository.EntityFrameworkCore.IntegrationTests
{
	/// <summary>
	/// Test fixture
	/// </summary>
	public class RepositoryTestsFixture
	{
		protected static readonly CancellationToken _cancellationToken 
			= CancellationToken.None;
		protected static readonly FakeAggregateRootEqualityComparer _fakeAggregateRootEqualityComparer 
			= new FakeAggregateRootEqualityComparer();
		
		/// <summary>
		/// Before any test
		/// </summary>
		[SetUp]
		public void Setup()
		{
			const string databaseName = "fakeDatabase";
			DbContextOptions<FakeDbContext> options = new DbContextOptionsBuilder<FakeDbContext>()
				.UseInMemoryDatabase(databaseName)
				.Options;
			
			DbContext = new FakeDbContext(options);
			Preconditions = new Preconditions(DbContext);
		}

		/// <summary>
		/// After any test
		/// </summary>
		[TearDown]
		public void Down()
		{
			DbContext.Database.EnsureDeleted();
			DbContext.Dispose();
		}

		/// <summary>
		/// Testing preconditions
		/// </summary>
		protected Preconditions Preconditions { get; private set; }
		
		/// <summary>
		/// EntityFramework Core database context  
		/// </summary>
		protected FakeDbContext DbContext { get; private set; }

		/// <summary>
		/// Creates and returns temporary testing repository scope
		/// </summary>
		/// <returns><see cref="RepositoryScope"/></returns>
		protected RepositoryScope CreateScope()
		{
			return new RepositoryScope(DbContext);
		}
	}
}
