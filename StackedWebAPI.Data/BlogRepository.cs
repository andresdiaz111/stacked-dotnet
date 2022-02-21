using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StackedWebAPI.Data.Models;
using StackedWebAPI.Models;

namespace StackedWebAPI.Data
{
    public class BlogRepository<T> : IBlogRepository<T> where T : EntityModel
    {
        private readonly DbSet<T> _entities;
        private readonly BlogDbContext _db;

        public BlogRepository(BlogDbContext db)
        {
            _entities = db.Set<T>();
            _db = db;
        }

        /// <summary>
        /// Create a new entity in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<Guid> Create(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.CreatedOn = DateTime.UtcNow;
            entity.UpdatedOn = DateTime.UtcNow;
            _entities.Add(entity);
            await _db.SaveChangesAsync();
            return entity.Id;
        }
        /// <summary>
        /// Delete a data given a ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentNullException(nameof(id));

                var entity = await _entities.SingleOrDefaultAsync(ent => ent.Id == id);
                _entities.Remove(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<PaginationResult<T>> GetAll(int page = 1, int perPage = 3)
        {
            var count = await _entities.CountAsync();
            var entsToSkip = (page - 1) * perPage;
            var entities = await _entities
                .OrderByDescending(ent => ent.UpdatedOn)
                .Skip(entsToSkip).Take(perPage)
                .ToListAsync();
            return new PaginationResult<T>
            {
                TotalCount = count,
                Results = entities,
                ResultPerPage = perPage,
                PageNumber = page
            };
        }

        public async Task<PaginationResult<T>> GetAll(int page, int perPage, Expression<Func<T, bool>> whereExp)
        {
            var count = await _entities.CountAsync();
            var entsToSkip = (page - 1) * perPage;
            var entities = await _entities
                .Where(whereExp)
                .OrderByDescending(ent => ent.UpdatedOn)
                .Skip(entsToSkip).Take(perPage)
                .ToListAsync();
            return new PaginationResult<T>
            {
                TotalCount = count,
                Results = entities,
                ResultPerPage = perPage,
                PageNumber = page
            };
        }

        public async Task<List<T>> GetAllWhere<TOrder>(Expression<Func<T, bool>> whereExp, Expression<Func<T, TOrder>> orderByExp, int limit = 1000)
        {
            var entities = _entities.AsQueryable().Where(whereExp);
            return await entities
                .Take(limit)
                .OrderByDescending(orderByExp)
                .ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _entities.SingleOrDefaultAsync(ent => ent.Id == id);
        }
        

        public async Task<T> GetFirstWhere<TOrder>(Expression<Func<T, bool>> whereExp, Expression<Func<T, TOrder>> orderByExp)
        {
            return await _entities
                .Where(whereExp)
                .OrderByDescending(orderByExp)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Update a resource in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<T> Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.UpdatedOn = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

