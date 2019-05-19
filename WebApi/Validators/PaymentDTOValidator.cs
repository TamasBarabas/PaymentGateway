using FluentValidation;
using TamasBarabas_Checkout.Model;

namespace TamasBarabas_Checkout
{
    public class PaymentDTOValidator : AbstractValidator<PaymentDTO>
    {
        public PaymentDTOValidator()
        {
            RuleFor(p => p.CardNumber)
                .NotEmpty()
                .CreditCard();

            RuleFor(p => p.ExpiryMonth)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(12);

            RuleFor(p => p.ExpiryYear)
                .NotEmpty()
                .GreaterThanOrEqualTo(2019)
                .LessThanOrEqualTo(2040);

            RuleFor(p => p.Currency)
                .NotEmpty()
                .Length(3);

            RuleFor(p => p.AmountInCents)
                .NotEmpty();

            RuleFor(p => p.Cvv)
                .NotEmpty()
                .GreaterThanOrEqualTo(100)
                .LessThanOrEqualTo(999);
        }
    }
}
