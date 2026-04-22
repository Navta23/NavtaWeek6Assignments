using BECSystem.DTOs.Auth;

namespace BECSystem.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto model);
        Task<string> LoginAsync(LoginDto model);
    }
}
