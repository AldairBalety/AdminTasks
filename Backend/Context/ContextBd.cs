using MySql.Data.MySqlClient;
using System.Data;

public class MySqlDbContext
{
    private readonly string _connectionString;

    public MySqlDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<MySqlConnection?> GetConnectionAsync()
    {
        try
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
        catch (MySqlException Ex)
        {
            Console.WriteLine(Ex.Message);
        }
        return null;
    }
    public async Task<DataTable> ExecuteStoredProcedureAsync(string procedureName, Dictionary<string, object>? parameters = null)
    {
        try
        {
            await using var connection = await GetConnectionAsync();
            await using var command = new MySqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
                foreach (var param in parameters)
                    command.Parameters.AddWithValue($"@{param.Key}", param.Value ?? DBNull.Value);

            var dataAdapter = new MySqlDataAdapter(command);
            var dataTable = new DataTable();
            await Task.Run(() => dataAdapter.Fill(dataTable));
            return dataTable;
        }
        catch (Exception Ex)
        {
            throw new ApplicationException($"Action Not Completed: {Ex.Message}");
        }
    }
    public async Task ExecuteNonQueryStoredProcedureAsync(string procedureName, Dictionary<string, object>? parameters = null)
    {
        try
        {
            using var connection = await GetConnectionAsync();
            var command = new MySqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
                foreach (var param in parameters)
                    command.Parameters.AddWithValue($"@{param.Key}", param.Value ?? DBNull.Value);

            await command.ExecuteNonQueryAsync();
        }
        catch (Exception Ex)
        {
            throw new ApplicationException($"Action Not Completed: {Ex.Message}");
        }
    }
}
