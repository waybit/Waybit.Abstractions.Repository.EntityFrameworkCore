using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;
using Waybit.Abstractions.Repository.EntityFrameworkCore.IntegrationTests.Fakes;
using Waybit.Abstractions.Repository.EntityFrameworkCore.IntegrationTests.Testing;

namespace Waybit.Abstractions.Repository.EntityFrameworkCore.IntegrationTests
{
	/// <summary>
	/// Repository tests
	/// </summary>
	public class RepositoryTests : RepositoryTestsFixture
	{
		/// <summary>
		/// Checks getting of all elements
		/// </summary>
		[Test]
		public async Task Can_get_all()
		{
			// Arrange
			Preconditions.EnsureFakes(10);
			List<FakeAggregateRoot> expected = DbContext
				.FakeAggregateRoots
				.ToList();
			
			// Act
			List<FakeAggregateRoot> actual;
			using (RepositoryScope scope = CreateScope())
			{
				actual = (await scope.Repository.GetAllAsync(_cancellationToken))
					?.ToList();
			}

			// Assert
			actual.ShouldNotBeNull();
			actual.Count.ShouldBe(expected.Count);
			actual.SequenceEqual(expected).ShouldBeTrue();
		}
		
		/// <summary>
		/// Checks getting of element by id
		/// </summary>
		[Test]
		public async Task Can_get_by_id()
		{
			// Arrange
			const int fakeAggregateRootId = 1;
			Preconditions.EnsureRandomFake();
			FakeAggregateRoot expected = await DbContext
				.FakeAggregateRoots
				.FindAsync(fakeAggregateRootId);
			
			// Act
			FakeAggregateRoot actual;
			using (RepositoryScope scope = CreateScope())
			{
				actual = await scope
					.Repository
					.GetByIdAsync(
						fakeAggregateRootId,
						_cancellationToken);
			}
			
			// Assert
			actual.ShouldNotBeNull();
			_fakeAggregateRootEqualityComparer.Equals(actual, expected).ShouldBeTrue();
		}
		
		/// <summary>
		/// Checks getting of null element
		/// </summary>
		[Test]
		public async Task Can_get_by_id_not_found()
		{
			// Arrange
			const int notFoundId = 11;
			Preconditions.EnsureRandomFake();
			
			// Act
			FakeAggregateRoot actual;
			using (RepositoryScope scope = CreateScope())
			{
				actual = await scope
					.Repository
					.GetByIdAsync(
						notFoundId,
						_cancellationToken);
			}
			
			// Assert
			actual.ShouldBeNull();
		}

		/// <summary>
		/// Checks for element addition
		/// </summary>
		[Test]
		public async Task Can_add()
		{
			// Arrange
			var newFakeAggregateRoot = new FakeAggregateRoot("expectedName");
			
			// Act
			int actual;
			using (RepositoryScope scope = CreateScope())
			{
				actual =  await scope
					.Repository
					.AddAsync(newFakeAggregateRoot, _cancellationToken);
			}
			
			// Assert
			FakeAggregateRoot expected = await DbContext
				.FakeAggregateRoots
				.FirstAsync(f => f.Name == newFakeAggregateRoot.Name);
			
			actual.ShouldBe(expected.Id);
		}
		
		/// <summary>
		/// Checks for element update
		/// </summary>
		[Test]
		public async Task Can_update()
		{
			// Arrange
			const string updatedName = "updatedName";
			Preconditions.EnsureRandomFake();
			FakeAggregateRoot existed = await DbContext
				.FakeAggregateRoots
				.FirstOrDefaultAsync();
			
			// Act
			using (RepositoryScope scope = CreateScope())
			{
				existed.ChangeName(updatedName);
				await scope
					.Repository
					.UpdateAsync(existed, _cancellationToken);
			}
			
			// Assert
			FakeAggregateRoot expected = await DbContext
				.FakeAggregateRoots
				.SingleAsync(f => f.Id == existed.Id, _cancellationToken);
			
			expected.Name.ShouldBe(updatedName);
		}
	}
}
