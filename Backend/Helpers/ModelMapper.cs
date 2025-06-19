using System.Data;
using Backend.Models;

namespace Backend.Helpers
{
    public static class ModelMapper
    {
        public static async Task<List<UserTask>> SetListTask(DataTable dataTable)
        {
            var tasks = new List<UserTask>();
            
            foreach (DataRow row in dataTable.Rows)
            {
                tasks.Add(new UserTask
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Title = row["Title"].ToString() ?? "",
                    Description = row["Description"].ToString() ?? "",
                    Completed = Convert.ToBoolean(row["Completed"]),
                    UserId = Convert.ToInt32(row["UserId"])
                });
            }
            
            return tasks;
        }

        public static async Task<User> SetUser(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0)
                throw new InvalidOperationException("No user data found");

            var row = dataTable.Rows[0];
            return new User
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString() ?? "",
                LastName = row["LastName"].ToString() ?? "",
                Email = row["Email"].ToString() ?? ""
            };
        }
    }
}
