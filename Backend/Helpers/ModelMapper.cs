namespace Backend.Helpers;

using System.Data;
using Backend.Models;

public class ModelMapper
{
    public static Task<User> SetUser(DataTable dataTable) => Task.FromResult(new User
    {
        Id = Convert.ToInt32(dataTable.Rows[0]["Id"] ?? 0),
        Name = dataTable.Rows[0]["Name"]?.ToString() ?? string.Empty,
        LastName = dataTable.Rows[0]["LastName"]?.ToString() ?? string.Empty,
        Email = dataTable.Rows[0]["Email"]?.ToString() ?? string.Empty
    });
    public static Task<List<UserTask>> SetListTask(DataTable dataTable)
    {
        var userTasks = new List<UserTask>();

        foreach (DataRow row in dataTable.Rows)
        {
            var userTask = new UserTask
            {
                Id = Convert.ToInt32(row["Id"] ?? 0),
                Title = row["Title"]?.ToString() ?? string.Empty,
                Description = row["Description"]?.ToString() ?? string.Empty,
                Completed = Convert.ToBoolean(row["Completed"] ?? false),
                Created_at = row["CreatedAt"] as DateTime?,
                Updated_at = row["UpdatedAt"] as DateTime?
            };
            userTasks.Add(userTask);
        }

        return Task.FromResult(userTasks);
    }
}