namespace DfcuPaymentGateway.Models
{
    public class PaymentStatusResponse
    {
        public int statusCode { get; set; }
        public required Pay pay { get; set; }
    }
}
