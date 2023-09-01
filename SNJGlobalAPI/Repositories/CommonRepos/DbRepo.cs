using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Serilog.Context;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using System.Linq.Expressions;
using SNJGlobalAPI.DbModels.SNJContext;
using SNJGlobalAPI.DtoModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SNJGlobalAPI.Repositories.CommonRepos
{
    public class DbRepo : IDb
    {
        private readonly GlobalAPIContext _db;
        private readonly ILogger<DbRepo> _logger;
        private readonly IMapper _mapper;
        public DbRepo(GlobalAPIContext db, ILogger<DbRepo> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public GlobalAPIContext Db() => _db;

        #region Transaction Setting
        public async Task<IDbContextTransaction> BeginTranAsync()
        {
            return await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTranAsync(IDbContextTransaction transaction)
        {
            await transaction.CommitAsync();
        }

        public async Task RollbackTranAsync(IDbContextTransaction transaction)
        {
            await transaction.RollbackAsync();
        }

        #endregion

        #region Getall
        public async Task<List<T>> GetAllAsync<T>(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
            Expression<Func<T, object>> orderBy = null,
            bool IsAsending = true,
            int? page = null) where T : class
        {
            IQueryable<T> query = _db.Set<T>();
            //checking if any including table
            if (includes is not null)
                query = includes(query);

            //selection
            if (predicate is not null)
                query = query.Where(predicate);

            //ordering
            if (orderBy is not null)
                query = IsAsending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            //pagination
            if (page is not null && page > 0)
            {
                int skip = (int)page * PagingInfo.take - PagingInfo.take;
                query = query.Skip(skip).Take(PagingInfo.take);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<List<TReturn>> GetAllByAsync<T, TReturn>(
            MapperConfiguration configuration = null,
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, object>> orderBy = null,
            bool IsAsending = true,
            int? page = null) where T : class where TReturn : class
        {
            IQueryable<T> query = _db.Set<T>();

            //selection
            if (predicate is not null)
                query = query.Where(predicate);

            //order by
            if (orderBy is not null)
                query = IsAsending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            //pagination
            if (page is not null && page > 0)
            {
                int skip = (int)page * PagingInfo.take - PagingInfo.take;
                query = query.Skip(skip).Take(PagingInfo.take);
            }

            if (configuration is null)
                configuration = new(c => c.CreateProjection<T, TReturn>());

            //projection
            return await query.ProjectTo<TReturn>(configuration).AsNoTracking().ToListAsync();
        }

        #endregion

        #region Get, GetByProjection
        public async Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
            Expression<Func<T, object>> orderBy = null,
            bool IsAsending = true) where T : class
        {
            IQueryable<T> query = _db.Set<T>();
            //includes
            if (includes is not null)
                query = includes(query);

            if (orderBy is not null)
                query = IsAsending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            return await query.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<TReturn> GetByAsync<T, TReturn>(
            Expression<Func<T, bool>> predicate,
            MapperConfiguration configuration = null,
            Expression<Func<T, object>> orderBy = null,
            bool IsAsending = true) where T : class where TReturn : class
        {
            IQueryable<T> query = _db.Set<T>();
            //includes 

            //selection
            if (predicate is not null)
                query = query.Where(predicate);

            if (orderBy is not null)
                query = IsAsending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            if (configuration is null)
                configuration = new(c => c.CreateProjection<T, TReturn>());

            //projection
            return await query.ProjectTo<TReturn>(configuration).AsNoTracking().FirstOrDefaultAsync();
        }


        public async Task<bool> IsAnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class
            => await _db.Set<T>().Where(predicate).AnyAsync();

        #endregion

        #region Post,Update & Delete

        public async Task<bool> PostRangeAsync<T>(List<T> entities) where T : class
        {
            await _db.Set<T>().AddRangeAsync(entities);
            if (await _db.SaveChangesAsync() > 0)
                return true;

            return false;
        }


        public async Task<bool> PostAsync<T>(T entity) where T : class
        {
            await _db.Set<T>().AddRangeAsync(entity);

            if (await _db.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> UpdateAsync<T>(T entity) where T : class
        {
            _db.Entry(entity).State = EntityState.Modified;

            if (await _db.SaveChangesAsync() > 0)
                return true;

            return false;
        }

        public async Task<bool> UpdateRangeAsync<T>(List<T> entity) where T : class
        {
            _db.UpdateRange(entity);
            if (await _db.SaveChangesAsync() > 0)
                return true;

            return false;
        }



        public async Task<bool> DeleteAsync<T>(T entities) where T : class
        {
            _db.Set<T>().RemoveRange(entities);
            if (await _db.SaveChangesAsync() > 0)
                return true;

            return false;
        }

        public async Task<bool> SqlQueryAsync(string query, params object[] obj)
        {
            if (await _db.Database.ExecuteSqlRawAsync(query, obj) > 0)
            {
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public Task<List<TReturn>> GetAllByAsync<T, TReturn>(MapperConfiguration configuration, Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> orderBy = null, bool IsAsending = true, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null, int? page = null)
            where T : class
            where TReturn : class
        {
            throw new NotImplementedException();
        }


        #endregion

        public async Task<Dictionary<string, object[]>> UpdateDataAndTrackChangesAsync<T>(T entity) where T : class
        {
            try
            {
                // assuming you have a DbContext object named "dbContext" and an entity object named "entity"
                // mark the entity as "modified" before making changes
                _db.Entry(entity).State = EntityState.Modified;

                // get the ChangeTracker object
                ChangeTracker changeTracker = _db.ChangeTracker;
                // get the list of all modified entities
                IEnumerable<EntityEntry> modifiedEntities = changeTracker.Entries()
                    .Where(e => e.State == EntityState.Modified);

                // create a dictionary to store updated properties
                Dictionary<string, object[]> updatedProperties = new Dictionary<string, object[]>();

                // loop through the modified entities
                foreach (EntityEntry modifiedEntity in modifiedEntities)
                {
                    // get the list of modified properties for this entity
                    IEnumerable<PropertyEntry> modifiedProperties = modifiedEntity.Properties
                        .Where(p => p.IsModified);

                    // loop through the modified properties and add them to the dictionary
                    foreach (PropertyEntry modifiedProperty in modifiedProperties)
                    {
                        string propertyName = modifiedProperty.Metadata.Name;
                        object originalValue = modifiedProperty.OriginalValue;
                        object currentValue = modifiedProperty.CurrentValue;

                        // check if the property was actually updated
                        if (!object.Equals(originalValue, currentValue))
                        {
                            updatedProperties.Add(propertyName, new object[] { originalValue, currentValue });
                        }
                    }
                }

                await _db.SaveChangesAsync();

                return updatedProperties;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<T> GetByTrackAsync<T>(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null, Expression<Func<T, object>> orderByDesc = null) where T : class
        {
            try
            {
                IQueryable<T> query = _db.Set<T>();
                //includes
                if (includes is not null)
                    query = includes(query);

                if (orderByDesc is not null)
                    query = query.OrderByDescending(orderByDesc);

                return await query.FirstOrDefaultAsync(predicate);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }//class
}