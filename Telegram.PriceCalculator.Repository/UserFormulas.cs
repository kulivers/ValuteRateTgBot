using MySqlConnector;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Repository;

public sealed class UserFormulasRepository : RepositoryBase<UserFormulaDto, string>
{
    private const string TableName = "userformulas";
    private const string FormulaIdColumn = "formulaId";
    private const string UserIdColumn = "userId";
    private const string FormulaColumn = "formula";

    public UserFormulasRepository(string connectionString) : base(connectionString)
    {
    }

    public override async Task Initialize()
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();
        var allDatabases = ReadStringAsync(CommonCommands.GetAllDatabases);
        var enumerator = allDatabases.GetAsyncEnumerator();
        while (await enumerator.MoveNextAsync())
        {
            var dbName = enumerator.Current;
            if (dbName == TableName)
            {
                return;
            }
        }

        var tableCmd = $"CREATE TABLE IF NOT EXISTS {TableName} (\n" +
                       $"{FormulaIdColumn} INT AUTO_INCREMENT PRIMARY KEY,\n" +
                       $"{UserIdColumn} BIGINT,\n" +
                       $"{FormulaColumn} VARCHAR(1024)\n);";
        await ExecuteAsync(tableCmd);
    }

    public override async Task<UserFormulaDto> Get(string entityId)
    {
        var getCmd = $"Select {FormulaIdColumn} {FormulaColumn} {UserIdColumn} FROM {TableName} WHERE {FormulaIdColumn} = {entityId}\n";
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();
        await using var command = new MySqlCommand(getCmd, connection);
        await using var reader = await command.ExecuteReaderAsync();
        string? formulaId = default;
        long userId = default;
        string? formula = default;
        while (await reader.ReadAsync())
        {
            formulaId = reader.GetString(0);
            formula = reader.GetString(1);
            userId = reader.GetInt64(2);
        }

        if (formula == default && userId == default || formulaId == default)
        {
            return new UserFormulaDto();
        }

        return new UserFormulaDto(userId, formulaId, formula);
    }

    public override async Task<UserFormulaDto> Create(UserFormulaDto entity)
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        var insertCmd = $"INSERT INTO {TableName} ({FormulaIdColumn}, {UserIdColumn}, {FormulaColumn}) VALUES({entity.FormulaId}, {entity.UserId}, {entity.Formula});";
        await ExecuteAsync(insertCmd);
        return entity;
    }

    public override async Task Edit(UserFormulaDto entity)
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        var insertCmd = $"REPLACE INTO {TableName} ({FormulaIdColumn}, {UserIdColumn}, {FormulaColumn}) VALUES({entity.FormulaId}, {entity.UserId}, {entity.Formula});";
        await ExecuteAsync(insertCmd);
    }

    public override async Task Delete(string entity)
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        var deleteCmd = $"DELETE FROM {TableName} WHERE {FormulaIdColumn} = {entity}\n";
        await ExecuteAsync(deleteCmd);
    }
}
