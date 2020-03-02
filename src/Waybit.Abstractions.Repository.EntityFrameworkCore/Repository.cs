using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Waybit.Abstractions.Domain;

namespace Waybit.Abstractions.Repository.EntityFrameworkCore
{
	/// <inheritdoc />
	public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
		where TKey : IEquatable<TKey>
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		private readonly DbContext _dbContext;

		/// <summary>
		/// Initialize instance of <see cref="Repository{TEntity,TKey}"/>
		/// </summary>
		/// <param name="dbContext">Entity framework database context</param>
		protected Repository(DbContext dbContext)
		{
			_dbContext = dbContext 
				?? throw new ArgumentNullException(nameof(dbContext));
		}

		/// <inheritdoc />
		public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
		{
			List<TEntity> entities = await _dbContext
				.Set<TEntity>()
				.ToListAsync(cancellationToken);

			return entities;
		}

		/// <inheritdoc />
		public virtual async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken)
		{
			TEntity entity = await _dbContext
				.Set<TEntity>()
				.FindAsync(
					keyValues: new object[] { id },
					cancellationToken: cancellationToken);

			return entity;
		}

		/// <inheritdoc />
		public virtual async Task<TKey> AddAsync(TEntity entity, CancellationToken cancellationToken)
		{
			EntityEntry<TEntity> entry = await _dbContext
				.Set<TEntity>()
				.AddAsync(entity, cancellationToken);
			
			return entry.Entity.Id;
		}

		/// <inheritdoc />
		public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			_dbContext.Update(entity);
		}
	}
}
