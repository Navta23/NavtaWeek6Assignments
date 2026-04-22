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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // ✅ Make payment (Student)
        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MakePayment(PaymentDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _paymentService.MakePaymentAsync(userId, dto);

            if (!result)
                return BadRequest("Course not found");

            return Ok("Payment successful");
        }

        // ✅ Get payments
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPayments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            var data = await _paymentService.GetPaymentsAsync(userId, role);

            return Ok(data);
        }
    }
}
