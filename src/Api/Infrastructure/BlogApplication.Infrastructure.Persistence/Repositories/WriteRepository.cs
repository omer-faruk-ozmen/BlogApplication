using BlogApplication.Api.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Domain.Models;
using BlogApplication.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Infrastructure.Persistence.Repositories
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;

        public WriteRepository(DbContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> Table => _context.Set<TEntity>();

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual int Add(TEntity entity)
        {
            Table.Add(entity);
            return _context.SaveChanges();
        }

        public virtual int Add(IEnumerable<TEntity> entities)
        {
            Table.AddRange(entities);
            return _context.SaveChanges();
        }

        public virtual async Task<int> AddAsync(IEnumerable<TEntity> entities)
        {
            await Table.AddRangeAsync(entities);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            Table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public int Update(TEntity entity)
        {
            Table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChanges();
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                Table.Attach(entity);
            }

            Table.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public int Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var entity = await Table.FindAsync(id);
            return await DeleteAsync(entity);
        }

        public virtual int Delete(Guid id)
        {
            var entity = Table.Find(id);
            return Delete(entity);
        }

        public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            _context.RemoveRange(Table.Where(predicate));
            return _context.SaveChanges() > 0;
        }

        public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            _context.RemoveRange(predicate);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual Task<int> AddOrUpdateAsync(TEntity entity)
        {
            if (!Table.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
                _context.Update(entity);

            return _context.SaveChangesAsync();

        }

        public virtual int AddOrUpdate(TEntity entity)
        {
            if (!Table.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
                _context.Update(entity);

            return _context.SaveChanges();
        }

        public virtual Task BulkDeleteById(IEnumerable<Guid>? ids)
        {
            if (ids != null && !ids.Any())
                return Task.CompletedTask;
            _context.RemoveRange(Table.Where(i => ids.Contains(i.Id)));
            return _context.SaveChangesAsync();
        }

        public virtual Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            _context.RemoveRange(Table.Where(predicate));
            return _context.SaveChangesAsync();
        }

        public virtual Task BulkDelete(IEnumerable<TEntity>? entities)
        {
            if (entities != null && !entities.Any())
                return Task.CompletedTask;

            Table.RemoveRange(entities);
            return _context.SaveChangesAsync();
        }

        public virtual Task BulkUpdate(IEnumerable<TEntity>? entities)
        {
            if (entities != null && !entities.Any())
                return Task.CompletedTask;
            foreach (var entity in entities!)
            {
                Table.Update(entity);
            }

            return _context.SaveChangesAsync();
        }

        public virtual async Task BulkAdd(IEnumerable<TEntity>? entities)
        {
            if (entities != null && !entities.Any())
                await Task.CompletedTask;

            await Table.AddRangeAsync(entities);

            await _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
