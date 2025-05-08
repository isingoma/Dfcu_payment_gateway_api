using DfcuPaymentGateway.Models;

namespace DfcuPaymentGateway.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponse> InitiatePaymentAsync(PaymentRequest request);
        Task<PaymentStatusResponse> GetPaymentStatusAsync(string transactionReference);
    }

}
