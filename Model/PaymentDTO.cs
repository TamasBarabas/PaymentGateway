
namespace TamasBarabas_Checkout.Model
{
    public class PaymentDTO
    {
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int AmountInCents { get; set; }
        public string Currency { get; set; }
        public int Cvv { get; set; }
    }
}
