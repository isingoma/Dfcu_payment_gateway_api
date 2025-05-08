namespace DfcuPaymentGateway.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string TransactionReference { get; set; }
        public string Payer { get; set; }
        public string Payee { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PayerReference { get; set; }
        public string Status { get; set; } // PENDING, SUCCESSFUL, FAILED
        public DateTime CreatedAt { get; set; }
    }

}
