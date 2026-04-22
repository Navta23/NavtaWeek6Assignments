using BECSystem.DTOs.Auth;
using BECSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace BECSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _authService.RegisterAsync(model);

            if (!result)
                return BadRequest("Registration failed");

            return Ok("User registered successfully");
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var token = await _authService.LoginAsync(model);

            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { Token = token });
        }

        [HttpGet]
        [Route("/api/student")]   // 🔥 absolute route
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _userService.GetAllStudentsAsync();
            return Ok(students);
        }

    }
}
