using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace SampleAuthWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpPost]
        public IActionResult Authenticate([FromBody] Credential credential)
        {
            // verify the credential
            if (credential.UserName == "admin" && credential.Password == "password")
            {
                //  Creating the security context JWT ONLY contains claims info - nothing else
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@mywebsite.com"),
                    new Claim("Department", "HR"),
                    new Claim("Admin", "True"),
                    new Claim("Manager", "True"),
                    new Claim("EmploymentDate", "2023-05-01")
                };

                // jwt also needs expiry time
                var expiresAt = DateTime.UtcNow.AddMinutes(10);

                return Ok(new
                {
                    access_token = CreateToken(claims, expiresAt),
                    expires_at = expiresAt
                });
            }

            ModelState.AddModelError("Unauthorized", "You are not authorized to access the endpoint.");
            return Unauthorized(ModelState);
        }

        private string CreateToken(IEnumerable<Claim> claims, DateTime expireAt)
        {
            // converts string to bytes (because there might be a null check, added the ?? "" at the end
            var secretKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretKey")??"");

            // generate the JWT
            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expireAt,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
                );

            // we need a plain string for the jwt but we only have an object.  This method will return a string
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }


    }

    public class Credential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
