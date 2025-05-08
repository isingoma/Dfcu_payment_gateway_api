using DfcuPaymentGateway.Data;
using DfcuPaymentGateway.Models;
using Microsoft.EntityFrameworkCore;

namespace DfcuPaymentGateway.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentsDbContext _context;
        private readonly Random _rand = new();

        public PaymentService(PaymentsDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentResponse> InitiatePaymentAsync(PaymentRequest request)
        {
            await Task.Delay(100); // Simulate minimum 100ms delay

            var refId = Guid.NewGuid().ToString("N").ToUpper();
            var roll = _rand.Next(1, 101); // 1-100

            string status;
            int code;
            string message;

            if (roll <= 10)
            {
                status = "PENDING";
                code = 100;
                message = "Transaction Pending";
            }
            else if (roll <= 95)
            {
                status = "SUCCESSFUL";
                code = 200;
                message = "Transaction successfully processed";
            }
            else
            {
                status = "FAILED";
                code = 400;
                message = "Transaction failed: Insufficient funds";
            }

            var payment = new Payment
            {
                TransactionReference = refId,
                Payer = request.Payer,
                Payee = request.Payee,
                Amount = request.Amount,
                Currency = request.Currency,
                PayerReference = request.PayerReference,
                Status = status,
                CreatedAt = DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return new PaymentResponse
            {
                statusCode = code,
                pay = new Pay
                {
                    transactionReference = refId,
                    message = message
                }
            };
        }

        public async Task<PaymentStatusResponse> GetPaymentStatusAsync(string transactionReference)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.TransactionReference == transactionReference);

            if (payment == null)
            {
                return new PaymentStatusResponse
                {
                    statusCode = 404,
                    pay = new Pay
                    {
                        transactionReference = transactionReference,
                        message = "NOT FOUND"
                    }
                };


            }

            int code = payment.Status switch
            {
                "PENDING" => 100,
                "SUCCESSFUL" => 200,
                "FAILED" => 400,
                _ => 500
            };

            return new PaymentStatusResponse
            {

                statusCode = code,
                pay = new Pay
                {
                    transactionReference = transactionReference,
                    message = payment.Status == "FAILED"
                    ? "Transaction failed: Check payer balance"
                    : $"Transaction {payment.Status}"
                }
            };
        }
    }

}
