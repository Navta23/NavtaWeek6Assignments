using BECSystem.Models;

namespace BECSystem.Services
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetAllStudentsAsync();
        Task<ApplicationUser> GetUserByIdAsync(string userId);
    }
}
