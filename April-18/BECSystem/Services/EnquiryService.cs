using BECSystem.Data;
using BECSystem.DTOs;
using BECSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BECSystem.Services
{
    public class EnquiryService : IEnquiryService
    {
        private readonly AppDbContext _context;

        public EnquiryService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ ADD ENQUIRY (with limit)
        public async Task<bool> AddEnquiryAsync(string userId, EnquiryDto dto)
        {
            var today = DateTime.UtcNow.Date;

            var count = await _context.Enquiries
                .Where(e => e.UserId == userId && e.EnquiryDate.Date == today)
                .CountAsync();

            if (count >= 5)
                return false; // limit reached ❌

            var enquiry = new Enquiry
            {
                UserId = userId,
                Title = dto.Title,
                Description = dto.Description,
                EnquiryType = dto.EnquiryType,
                EnquiryDate = DateTime.UtcNow,
                Status = "Pending"
            };

            _context.Enquiries.Add(enquiry);
            await _context.SaveChangesAsync();

            return true;
        }

        // ✅ GET ENQUIRIES
        public async Task<List<Enquiry>> GetEnquiriesAsync(string userId, string role)
        {
            if (role == "Admin")
            {
                return await _context.Enquiries
                    .Include(e => e.User)
                    .ToListAsync();
            }

            return await _context.Enquiries
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        // ✅ DELETE ENQUIRY (Student)
        public async Task<bool> DeleteEnquiryAsync(int enquiryId, string userId)
        {
            var enquiry = await _context.Enquiries
                .FirstOrDefaultAsync(e => e.EnquiryID == enquiryId && e.UserId == userId);

            if (enquiry == null)
                return false;

            _context.Enquiries.Remove(enquiry);
            await _context.SaveChangesAsync();

            return true;
        }

        // ✅ UPDATE STATUS (Admin)
        public async Task<bool> UpdateStatusAsync(int enquiryId, string status)
        {
            var enquiry = await _context.Enquiries.FindAsync(enquiryId);

            if (enquiry == null)
                return false;

            enquiry.Status = status;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
