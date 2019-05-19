using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TamasBarabas_Checkout.Model;

namespace TamasBarabas_Checkout.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentGateway paymentGateway;

        public PaymentController(IPaymentGateway paymentGateway)
        {
            this.paymentGateway = paymentGateway;
        }

        // GET api/values/5
        [HttpGet("{paymentId}")]
        public async Task<IActionResult> Get(Guid paymentId)
        {
            var payment = await paymentGateway.FindPaymentAsync(paymentId);
            if (payment == null)
            {
                return NotFound(); // Returns a NotFoundResult
            }
            return Ok(payment);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentDTO dto)
        {
            var payment = await paymentGateway.PayAsync(dto);
            if (payment.PaymentStatus==PaymentStatusOptions.Failed)
            {
                return BadRequest(payment); // Returns a NotFoundResult
            }
            return Ok(payment);
        }
    }
}
