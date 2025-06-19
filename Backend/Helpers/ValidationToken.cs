using Backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Backend.Helpers
{
    public class ValidationToken
    {
        public async Task<User> ValidationAsync(HttpContext httpContext, JwtTokenService jwtTokenService, MySqlDbContext mySqlDbContext)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token not provided");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadJwtToken(token);
            var userId = jsonToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            var parameters = new Dictionary<string, object>
            {
                { "inputUserId", int.Parse(userId) }
            };
            
            var userTable = await mySqlDbContext.ExecuteStoredProcedureAsync("GetUserById", parameters);
            
            if (userTable.Rows.Count == 0)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            return ModelMapper.SetUser(userTable).Result;
        }
    }
}
