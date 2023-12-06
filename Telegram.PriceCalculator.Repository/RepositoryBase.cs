using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Telegram.PriceCalculator.Repository;

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

public abstract class RepositoryBase
{
    protected readonly string _connectionString;

    public const string DefaultDatabaseName = "valutebot";

    protected RepositoryBase(string connectionString)
    {
        _connectionString = connectionString;
        CreateDatabase(DefaultDatabaseName);
    }

    private void CreateDatabase(string name)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        var allDatabases = ReadString(CommonCommands.GetAllDatabases);
        if (!allDatabases.Contains(DefaultDatabaseName))
        {
            Execute(string.Format(CommonCommands.CreateDatabase, name));
        }
    }

    public async Task<bool> TestConnection()
    {
        try
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async IAsyncEnumerable<string> ReadStringAsync(string toExec)
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new MySqlCommand(toExec, connection);
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            yield return reader.GetString(0);
    }

    public IEnumerable<string> ReadString(string toExec)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        using var command = new MySqlCommand(toExec, connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
            yield return reader.GetString(0);
    }

    public void Execute(string toExec)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();
        using var command = new MySqlCommand(toExec, connection);
        command.ExecuteNonQuery();
    }

    public async Task ExecuteAsync(string toExec)
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new MySqlCommand(toExec, connection);
        await command.ExecuteNonQueryAsync();
    }
}

public abstract class RepositoryBase<T, TKey> : RepositoryBase
{
    public abstract Task Initialize();
    public abstract Task<T> Get(TKey entity);
    public abstract Task<T> Create(T entity);
    public abstract Task Edit(T entity);
    public abstract Task Delete(TKey entity);

    protected RepositoryBase(string connectionString) : base(connectionString)
    {
    }
}
