using System.Security.Claims;
using Backend.Helpers;
using Backend.Models;

namespace Backend;

public class ValidationToken
{
    public async Task<User> ValidationAsync(HttpContext httpContext, JwtTokenService jwtTokenService, MySqlDbContext mySqlDbContext)
    {
        var authHeader = httpContext.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            throw new UnauthorizedAccessException();

        var token = authHeader["Bearer ".Length..].Trim();
        var principal = jwtTokenService.ValidateToken(token)
            ?? throw new UnauthorizedAccessException(); ;

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException();

        var parameters = new Dictionary<string, object>
        {
            { "inputUserId", userId },
        };

        var procedure = "GetUserById";
        var userTable = await mySqlDbContext.ExecuteStoredProcedureAsync(procedure, parameters);

        if (userTable.Rows.Count > 0)
        {
            var user = ModelMapper.SetUser(userTable).Result;
            if (user.Id < 1)
                throw new UnauthorizedAccessException();
            return user;
        }

        throw new UnauthorizedAccessException();
    }
}
