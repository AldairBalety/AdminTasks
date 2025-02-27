using MySql.Data.MySqlClient;
using System.Data;

public class MySqlDbContext
{
    private readonly string _connectionString;
    private readonly ILogger<MySqlDbContext> _logger;

    public MySqlDbContext(string connectionString, ILogger<MySqlDbContext> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public async Task<MySqlConnection?> GetConnectionAsync()
    {
        try
        {
            _logger.LogInformation("Attempting to open a connection with connection string: {ConnectionString}", _connectionString);

            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            _logger.LogInformation("Connection established successfully.");
            return connection;
        }
        catch (MySqlException Ex)
        {
            _logger.LogError(Ex, "An error occurred while attempting to connect to the database.");
        }
        return null;
    }

    public async Task<DataTable> ExecuteStoredProcedureAsync(string procedureName, Dictionary<string, object>? parameters = null)
    {
        try
        {
            await using var connection = await GetConnectionAsync();
            if (connection == null)
            {
                _logger.LogError("Database connection is null.");
                return new DataTable();
            }

            _logger.LogInformation("Executing stored procedure: {ProcedureName}", procedureName);

            await using var command = new MySqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue($"@{param.Key}", param.Value ?? DBNull.Value);
                    _logger.LogInformation("Adding parameter: {ParamKey} with value: {ParamValue}", param.Key, param.Value);
                }
            }

            var dataAdapter = new MySqlDataAdapter(command);
            var dataTable = new DataTable();
            await Task.Run(() => dataAdapter.Fill(dataTable));
            return dataTable;
        }
        catch (Exception Ex)
        {
            _logger.LogError(Ex, "An error occurred while executing the stored procedure.");
            throw new ApplicationException($"Action Not Completed: {Ex.Message}");
        }
    }

    public async Task ExecuteNonQueryStoredProcedureAsync(string procedureName, Dictionary<string, object>? parameters = null)
    {
        try
        {
            await using var connection = await GetConnectionAsync();
            if (connection == null)
            {
                _logger.LogError("Database connection is null.");
                return;
            }

            _logger.LogInformation("Executing non-query stored procedure: {ProcedureName}", procedureName);

            var command = new MySqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue($"@{param.Key}", param.Value ?? DBNull.Value);
                    _logger.LogInformation("Adding parameter: {ParamKey} with value: {ParamValue}", param.Key, param.Value);
                }
            }

            await command.ExecuteNonQueryAsync();
            _logger.LogInformation("Non-query execution completed successfully.");
        }
        catch (Exception Ex)
        {
            _logger.LogError(Ex, "An error occurred while executing the non-query stored procedure.");
            throw new ApplicationException($"Action Not Completed: {Ex.Message}");
        }
    }
}
