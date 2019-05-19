using System;

namespace TamasBarabas_Checkout.Model
{
    public class BankPaymentResult
    {
        public BankPaymentStatusOptions Status { get; }
        public Guid Id { get; }

        public BankPaymentResult(BankPaymentStatusOptions status, Guid id)
        {
            Status = status;
            Id = id;
        }
    }
}