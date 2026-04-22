using BECSystem.Data;
using BECSystem.DTOs;
using BECSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BECSystem.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> AddCourseAsync(CourseDto dto)
        {
            var course = new Course
            {
                CourseName = dto.CourseName,
                Description = dto.Description,
                Duration = dto.Duration,
                Cost = dto.Cost
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course;
        }

        public async Task<bool> UpdateCourseAsync(int id, CourseDto dto)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return false;

            course.CourseName = dto.CourseName;
            course.Description = dto.Description;
            course.Duration = dto.Duration;
            course.Cost = dto.Cost;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
