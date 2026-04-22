using BECSystem.DTOs;
using BECSystem.Models;

namespace BECSystem.Services
{
    public interface IPaymentService
    {
        Task<bool> MakePaymentAsync(string userId, PaymentDto dto);
        Task<List<Payment>> GetPaymentsAsync(string userId, string role);
    }
}
