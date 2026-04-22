using BECSystem.Data;
using BECSystem.DTOs;
using BECSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace BECSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ MAKE PAYMENT
        public async Task<bool> MakePaymentAsync(string userId, PaymentDto dto)
        {
            var course = await _context.Courses.FindAsync(dto.CourseId);

            if (course == null)
                return false;

            var payment = new Payment
            {
                UserId = userId,
                CourseId = dto.CourseId,
                TotalAmount = course.Cost,
                Status = "Paid", // simplified
                ModeOfPayment = dto.ModeOfPayment,
                PaymentDate = DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return true;
        }

        // ✅ GET PAYMENTS
        public async Task<List<Payment>> GetPaymentsAsync(string userId, string role)
        {
            if (role == "Admin")
            {
                return await _context.Payments
                    .Include(p => p.Course)
                    .Include(p => p.User)
                    .ToListAsync();
            }

            return await _context.Payments
                .Where(p => p.UserId == userId)
                .Include(p => p.Course)
                .ToListAsync();
        }
    }
}
