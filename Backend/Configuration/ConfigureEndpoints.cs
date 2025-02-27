namespace Backend.Configuration;

using Backend.Helpers;
using Backend.Models;

public static class ConfigureEndpoints
{
    public static void Configure(WebApplication app)
    {
        // Task Endpoints
        app.MapGet("/api/tasks", async (HttpContext httpContext, JwtTokenService jwtTokenService, MySqlDbContext mySqlDbContext) =>
        {
            var user = await new ValidationToken().ValidationAsync(httpContext, jwtTokenService, mySqlDbContext);
            var parameters = new Dictionary<string, object>
            {
                { "inputUserId", user.Id }
            };
            var taskTable = await mySqlDbContext.ExecuteStoredProcedureAsync("GetTasksByUserId", parameters);
            if (taskTable.Rows.Count > 0)
            {
                var tasks = ModelMapper.SetListTask(taskTable).Result;
                return Results.Ok(tasks);
            }
            return Results.Ok();
        });
        app.MapPost("/api/tasks", async (UserTask task, HttpContext httpContext, JwtTokenService jwtTokenService, MySqlDbContext mySqlDbContext) =>
        {
            var user = await new ValidationToken().ValidationAsync(httpContext, jwtTokenService, mySqlDbContext);
            var parameters = new Dictionary<string, object>
            {
                { "inputTitle", task.Title },
                { "inputDescription", task.Description ?? string.Empty },
                { "inputUserId", user.Id}
            };
            await mySqlDbContext.ExecuteNonQueryStoredProcedureAsync("AddTask", parameters);
            return Results.Ok();
        });
        app.MapPut("/api/tasks/{inputTaskId}", async (int inputTaskId, UserTask task, HttpContext httpContext, JwtTokenService jwtTokenService, MySqlDbContext mySqlDbContext) =>
        {
            var user = await new ValidationToken().ValidationAsync(httpContext, jwtTokenService, mySqlDbContext);
            var parameters = new Dictionary<string, object>
            {
                { "inputTaskId", inputTaskId },
                { "inputTitle", task.Title },
                { "inputDescription", task.Description ?? string.Empty },
                { "inputCompleted", task.Completed }
            };
            await mySqlDbContext.ExecuteNonQueryStoredProcedureAsync("UpdateTask", parameters);
            return Results.Ok();
        });
        app.MapDelete("/api/tasks/{inputTaskId}", async (int inputTaskId, HttpContext httpContext, JwtTokenService jwtTokenService, MySqlDbContext mySqlDbContext) =>
        {
            var user = await new ValidationToken().ValidationAsync(httpContext, jwtTokenService, mySqlDbContext);
            var parameters = new Dictionary<string, object>
            {
                { "inputTaskId", inputTaskId },
            };
            await mySqlDbContext.ExecuteNonQueryStoredProcedureAsync("DeleteTask", parameters);
            return Results.Ok();
        });
        // User Endpoints
        app.MapPost("/api/user/login", async (Login login, JwtTokenService jwtTokenService, MySqlDbContext mySqlDbContext) =>
        {
            var parameters = new Dictionary<string, object>
            {
                { "inputEmail", login.Username },
                { "inputPassword", login.Password }
            };
            var userTable = await mySqlDbContext.ExecuteStoredProcedureAsync("LoginUser", parameters);

            if (userTable.Rows.Count > 0)
            {
                var user = ModelMapper.SetUser(userTable).Result;
                var token = jwtTokenService.GenerateToken(user.Id.ToString(), user.Name);
                return Results.Ok(new { Token = token });
            }

            return Results.Unauthorized();
        });
        app.MapGet("/api/user", async (HttpContext httpContext, JwtTokenService jwtTokenService, MySqlDbContext mySqlDbContext) =>
        {
            var user = await new ValidationToken().ValidationAsync(httpContext, jwtTokenService, mySqlDbContext);
            return Results.Ok(user);
        });
        app.MapPut("/api/user", async (User newUser, HttpContext httpContext, JwtTokenService jwtTokenService, MySqlDbContext mySqlDbContext) =>
        {
            var user = await new ValidationToken().ValidationAsync(httpContext, jwtTokenService, mySqlDbContext);
            var parameters = new Dictionary<string, object>
            {
                { "inputUserId", user.Id },
                { "inputName", newUser.Name },
                { "inputLastName", newUser.LastName },
                { "inputEmail", newUser.Email },
                { "inputPassword", newUser.Password },
            };
            await mySqlDbContext.ExecuteNonQueryStoredProcedureAsync("UpdateUser", parameters);
            return Results.Ok();
        });
        app.MapPost("/api/user", async (User user, HttpContext httpContext, JwtTokenService jwtTokenService, MySqlDbContext mySqlDbContext) =>
        {
            var parameters = new Dictionary<string, object>
            {
                { "inputName", user.Name },
                { "inputLastName", user.LastName },
                { "inputEmail", user.Email },
                { "inputPassword", user.Password },
            };
            await mySqlDbContext.ExecuteNonQueryStoredProcedureAsync("AddUser", parameters);
            return Results.Ok();
        });
    }
}