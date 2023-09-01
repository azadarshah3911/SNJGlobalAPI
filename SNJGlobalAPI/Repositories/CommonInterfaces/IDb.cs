using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using SNJGlobalAPI.DbModels.SNJContext;
using System.Linq.Expressions;

namespace SNJGlobalAPI.Repositories.CommonInterfaces
{
    public interface IDb
    {
        GlobalAPIContext Db();
        Task<bool> SqlQueryAsync(string query, params object[] obj);
        Task<bool> IsAnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
            Expression<Func<T, object>> orderBy = null,
            bool IsAsending = true) where T : class;

        Task<TReturn> GetByAsync<T, TReturn>(
            Expression<Func<T, bool>> predicate,
            MapperConfiguration configuration = null,
            Expression<Func<T, object>> orderBy = null,
            bool IsAsending = true) where T : class where TReturn : class;

        Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
            Expression<Func<T, object>> orderBy = null,
            bool IsAsending = true,
            int? page = null) where T : class;


        Task<List<TReturn>> GetAllByAsync<T, TReturn>(
            MapperConfiguration configuration = null,
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, object>> orderBy = null,
            bool IsAsending = true,
            int? page = null) where T : class where TReturn : class;

        Task<bool> PostAsync<T>(T entity) where T : class;
        Task<bool> PostRangeAsync<T>(List<T> entities) where T : class;
        Task<bool> DeleteAsync<T>(T entities) where T : class;
        Task<bool> UpdateAsync<T>(T entity) where T : class;
        Task<bool> UpdateRangeAsync<T>(List<T> entity) where T : class;

        //Transactions Mgmt
        Task<IDbContextTransaction> BeginTranAsync();
        Task CommitTranAsync(IDbContextTransaction transaction);
        Task RollbackTranAsync(IDbContextTransaction transaction);

        //Task RawQuery(string query);

        //By Azadar
        Task<T> GetByTrackAsync<T>(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null, Expression<Func<T, object>> orderByDesc = null) where T : class;
        Task<Dictionary<string, object[]>> UpdateDataAndTrackChangesAsync<T>(T entity) where T : class;


    }
}