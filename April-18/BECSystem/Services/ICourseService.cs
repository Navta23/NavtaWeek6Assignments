using BECSystem.DTOs;
using BECSystem.Models;

namespace BECSystem.Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCoursesAsync();
        Task<Course> AddCourseAsync(CourseDto dto);
        Task<bool> UpdateCourseAsync(int id, CourseDto dto);
        Task<bool> DeleteCourseAsync(int id);
    }
}
