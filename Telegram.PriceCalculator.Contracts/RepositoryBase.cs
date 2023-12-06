using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Telegram.PriceCalculator.Repository;

namespace Telegram.PriceCalculator.Contracts;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext RepositoryContext;

    public RepositoryBase(RepositoryContext repositoryContext) => RepositoryContext = repositoryContext;

    public IQueryable<T> FindAll(bool trackChanges = false) =>
        !trackChanges
            ? RepositoryContext.Set<T>()
                               .AsNoTracking()
            : RepositoryContext.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges = true) =>
        !trackChanges
            ? RepositoryContext.Set<T>().Where(expression).AsNoTracking()
            : RepositoryContext.Set<T>().Where(expression);

    public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);

    public async Task CreateAsync(T entity) => await RepositoryContext.Set<T>().AddAsync(entity);

    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
    public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
}
