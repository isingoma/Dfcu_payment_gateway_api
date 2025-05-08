namespace DfcuPaymentGateway.Models
{
    public class Pay
    {
        public string transactionReference { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
    }

    public class PaymentResponse
    {
        public int statusCode { get; set; }
        public required Pay pay { get; set; }

    }
}
