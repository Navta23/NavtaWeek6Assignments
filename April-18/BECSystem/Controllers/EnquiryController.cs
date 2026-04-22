using BECSystem.DTOs;
using BECSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BECSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {
        private readonly IEnquiryService _enquiryService;

        public EnquiryController(IEnquiryService enquiryService)
        {
            _enquiryService = enquiryService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllEnquiries()
        {
            var data = await _enquiryService.GetEnquiriesAsync(null, "Admin");
            return Ok(data);
        }

        // ✅ Add enquiry (Student)
        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> AddEnquiry([FromBody] EnquiryDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _enquiryService.AddEnquiryAsync(userId, dto);

            if (!result)
                return BadRequest("Daily limit (5 enquiries) reached");

            return Ok("Enquiry submitted");
        }

        //// ✅ Get enquiries
        //[HttpGet]
        //[Authorize]
        //public async Task<IActionResult> GetEnquiries()
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var role = User.FindFirstValue(ClaimTypes.Role);

        //    var data = await _enquiryService.GetEnquiriesAsync(userId, role);

        //    return Ok(data);
        //}

        // ✅ Delete enquiry (Student)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> DeleteEnquiry(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _enquiryService.DeleteEnquiryAsync(id, userId);

            if (!result)
                return NotFound("Enquiry not found");

            return Ok("Deleted successfully");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> UpdateEnquiry(int id, [FromBody] EnquiryDto dto)
        {
            // optional logic if you implement update
            return Ok("Update logic here");
        }

        [HttpGet]
        [Route("/api/user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserEnquiries(string userId)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);

            var data = await _enquiryService.GetEnquiriesAsync(userId, role);

            return Ok(data);
        }

        // ✅ Update status (Admin)
        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var result = await _enquiryService.UpdateStatusAsync(id, status);

            if (!result)
                return NotFound("Enquiry not found");

            return Ok("Status updated");
        }
    }
}
