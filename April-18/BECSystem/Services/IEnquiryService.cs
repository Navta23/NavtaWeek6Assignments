using BECSystem.DTOs;
using BECSystem.Models;

namespace BECSystem.Services
{
    public interface IEnquiryService
    {
        Task<bool> AddEnquiryAsync(string userId, EnquiryDto dto);
        Task<List<Enquiry>> GetEnquiriesAsync(string userId, string role);
        Task<bool> DeleteEnquiryAsync(int enquiryId, string userId);
        Task<bool> UpdateStatusAsync(int enquiryId, string status);
    }
}
