using LDAP.API.Data;
using LDAP.API.Repositories;
using LDAP.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LDAP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly ILDAPService _ldapService;
        private readonly IUserRepository _userRepository;

        public AuthenticateController(ILDAPService ldapService, IUserRepository userRepository)
        {
            _ldapService = ldapService;
            _userRepository = userRepository;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequestDTO request)
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
    }
}

