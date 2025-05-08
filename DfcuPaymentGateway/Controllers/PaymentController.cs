using DfcuPaymentGateway.Models;
using DfcuPaymentGateway.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DfcuPaymentGateway.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("initiatepayment")]
        public async Task<IActionResult> InitiatePayment([FromBody] PaymentRequest request)
        {
            var response = await _paymentService.InitiatePaymentAsync(request);
            return StatusCode(response.statusCode, response);
        }

        [HttpGet("checkstatus/{transactionReference}")]
        public async Task<IActionResult> GetStatus(string transactionReference)
        {
            var response = await _paymentService.GetPaymentStatusAsync(transactionReference);
            return StatusCode(response.statusCode, response);
        }
    }
}
