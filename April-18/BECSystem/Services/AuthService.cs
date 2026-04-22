using BECSystem.DTOs.Auth;
using BECSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace BECSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        // REGISTER
        public async Task<bool> RegisterAsync(RegisterDto model)
        {
            if (model.Role != "Admin" && model.Role != "Student")
                return false;

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return false;

            //Assign Role HERE
            await _userManager.AddToRoleAsync(user, model.Role);

            return true;
        }

        //LOGIN
        public async Task<string> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return null;

            var isValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isValid)
                return null;

            // Get roles
            var roles = await _userManager.GetRolesAsync(user);

            // Generate JWT
            return _jwtService.GenerateToken(user, roles);
        }
    }
}
