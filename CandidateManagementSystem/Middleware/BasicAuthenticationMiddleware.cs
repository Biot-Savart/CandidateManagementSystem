using System.Net.Http.Headers;
using System.Text;

/**
 * SHOULD BE REPLACED WITH BETTER AUTHENTICATION
 * Security: Basic authentication without SSL/TLS (HTTPS) is not secure. Credentials can be easily intercepted over the network. 
 * Always use HTTPS when deploying your application to production.
 * Storage & Validation: The example uses hardcoded credentials, which is not secure for real applications. 
 * In a production scenario, you should store hashed passwords and validate against those hashes. Consider using ASP.NET Core Identity for a more comprehensive solution.
 * Alternatives: For more secure and flexible authentication and authorization, consider using JWT tokens or OAuth 2.0. 
 * These methods provide better security and are widely adopted in modern web applications.
 */

namespace CandidateManagementSystem.Middleware
{
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = 401; // Unauthorized
                return;
            }

            var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
            if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                authHeader.Parameter != null)
            {
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                if (IsUserValid(username, password))
                {
                    await _next(context);
                    return;
                }
            }

            context.Response.StatusCode = 401; // Unauthorized
        }

        private bool IsUserValid(string username, string password)
        {
            // Validate the username and password
            // This is where you would check against your user store
            // For demonstration, assuming "admin" as username and "password" as the password
            return username == "admin" && password == "password";
        }
    }
}
