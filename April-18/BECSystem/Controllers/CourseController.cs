using BECSystem.DTOs;
using BECSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BECSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // ✅ View all courses (Student + Admin)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("CourseName")]
        [Authorize]
        public async Task<IActionResult> GetCourseNames()
        {
            var courses = await _courseService.GetAllCoursesAsync();

            var names = courses.Select(c => c.CourseName);

            return Ok(names);
        }

        // ✅ Add course (Admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCourse(CourseDto dto)
        {
            var course = await _courseService.AddCourseAsync(dto);
            return Ok(course);
        }
        // ✅ Update course (Admin only)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCourse(int id, CourseDto dto)
        {
            var result = await _courseService.UpdateCourseAsync(id, dto);

            if (!result)
                return NotFound("Course not found");

            return Ok("Course updated");
        }

        // ✅ Delete course (Admin only)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseService.DeleteCourseAsync(id);

            if (!result)
                return NotFound("Course not found");
            return Ok("Course deleted");
        }

        [HttpGet]
        [Route("/api/Student/Course")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetCoursesForStudent()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }
    }
}
