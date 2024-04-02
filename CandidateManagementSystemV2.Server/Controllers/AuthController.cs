using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    // This example uses a hardcoded username and password.
    // Replace this with your actual authentication logic, e.g., database lookup.
    private const string ValidUsername = "admin";
    private const string ValidPassword = "password";

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login()
    {
        if (Request.Headers.ContainsKey("Authorization"))
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            var username = credentials[0];
            var password = credentials[1];

            if (username == ValidUsername && password == ValidPassword)
            {
                // Authentication successful
                // Here you would typically create a user object, perhaps include a JWT token, etc.
                return Ok(new { message = "Authentication successful" });
            }
        }

        // Authentication failed
        return Unauthorized(new { message = "Authentication failed" });
    }
}
