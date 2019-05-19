using System;

namespace TamasBarabas_Checkout.Model
{
    public class Payment
    {
        public Guid Id { get; }
        public DateTime PaymentDate { get; }
        public PaymentStatusOptions PaymentStatus { get; }

        public string CardNumber { get; }
        public int ExpiryMonth { get; }
        public int ExpiryYear { get; }
        public int AmountInCents { get; }
        public string Currency { get; }
        public int Cvv { get; }
        public Guid BankId { get; }

        public Payment(PaymentDTO payment)
        {
            Id = Guid.NewGuid();
            PaymentDate = DateTime.UtcNow;
            PaymentStatus = PaymentStatusOptions.New;

            CardNumber = payment.CardNumber;
            ExpiryYear = payment.ExpiryYear;
            ExpiryMonth = payment.ExpiryMonth;
            AmountInCents = payment.AmountInCents;
            Currency = payment.Currency;
            Cvv = payment.Cvv;
        }

        private Payment(Payment payment, PaymentStatusOptions status, Guid bankId)
        {
            Id = payment.Id;
            PaymentDate = payment.PaymentDate;
            PaymentStatus = status;

            CardNumber = payment.CardNumber;
            ExpiryYear = payment.ExpiryYear;
            ExpiryMonth = payment.ExpiryMonth;
            AmountInCents = payment.AmountInCents;
            Currency = payment.Currency;
            Cvv = payment.Cvv;
            BankId = bankId;
        }

        public Payment Confirm(Guid bankId)
        {
            return new Payment(this, PaymentStatusOptions.Confirmed, bankId);
        }
        public Payment Fail(Guid bankId)
        {
            return new Payment(this, PaymentStatusOptions.Failed, bankId);
        }

    }
}
