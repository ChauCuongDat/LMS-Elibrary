using LMS_Elibrary.Models;
using LMS_Elibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace LMS_Elibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            if (await _authService.Register(request))
            {
                return Ok("Registered");
            }
            return BadRequest("Something when wrong");
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string jwt = await _authService.Login(request);
            if(jwt == "false") 
            {
                return BadRequest("Some thing when wrong");
            }
            return Ok(jwt);
        }

    }
}
