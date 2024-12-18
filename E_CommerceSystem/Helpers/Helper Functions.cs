using System.IdentityModel.Tokens.Jwt;

namespace E_CommerceSystem.Helpers
{
    public static class Helper_Functions
    {

        public static string? ExtractTokenFromRequest(HttpRequest request)
        {
            // Get the Authorization header value
            var authorizationHeader = request.Headers["Authorization"].FirstOrDefault();

            // Check if the header exists and starts with "Bearer "
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return authorizationHeader.Substring("Bearer ".Length).Trim(); // Extract the token
            }

            return null; // Token not found
        }


        public static string GetUserIDFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);
                // Extract the 'role' claim
                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
                return roleClaim; // Return the role or null if not found
            }
            throw new UnauthorizedAccessException("Invalid or unreadable token.");
        }


        private static string GetUserRoleFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);
                // Extract the 'role' claim
                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role").Value;
                return roleClaim; // Return the role or null if not found
            }
            throw new UnauthorizedAccessException("Invalid or unreadable token.");
        }


    }
}
