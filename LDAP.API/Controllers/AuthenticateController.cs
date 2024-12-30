using LDAP.API.Data;
using LDAP.API.Repositories;
using LDAP.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LDAP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly ILDAPService _ldapService;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticateController(ILDAPService ldapService, IUserRepository userRepository, IConfiguration configuration)
        {
            _ldapService = ldapService;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("GenerateToken")]
        public IActionResult GenerateToken(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest(new AuthResultDTO
                    {
                        Errors = new List<string> { "Invalid request" },
                        Result = false
                    });
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _configuration["JwtSettings:Issuer"],
                    _configuration["JwtSettings:Audience"],
                    claims,
                    expires: System.DateTime.Now.AddHours(1),
                    signingCredentials: creds);

                return Ok(new AuthResultDTO { Token = new JwtSecurityTokenHandler().WriteToken(token), Result = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthResultDTO
                {
                    Errors = new List<string> { $"Error generating token { ex.Message }" },
                    Result = false
                });
            }
            
        }

        [Authorize]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequestDTO request)
        {
            try
            {
                if (_ldapService.AuthenticateUser(request.Username, request.Password))
                {
                    // Guardar en base de datos
                    var userInfo = new UserInfo
                    {
                        Username = request.Username,
                        Email = $"{request.Username}@example.com",
                        DisplayName = "John Doe",
                        Department = "IT",
                        Title = "Software Engineer"
                    };

                    _userRepository.SaveUser(userInfo);

                    return Ok(new
                    {
                        status = "success",
                        user = userInfo
                    });
                }

                return Unauthorized(new
                {
                    status = "error",
                    message = "Invalid credentials or user not found."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthResultDTO
                {
                    Errors = new List<string> { $"Error authenticating user {ex.Message}" },
                    Result = false
                });
            }
            
        }
    }
}

