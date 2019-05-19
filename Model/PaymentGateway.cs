using Model.Clients;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TamasBarabas_Checkout.Model
{

    public interface IPaymentGateway
    {
        Task<Payment> PayAsync(PaymentDTO dto);
        Task<Payment> FindPaymentAsync(Guid paymentId);
    }
    
    public class PaymentGateway : IPaymentGateway
    {
        private readonly IDictionary<Guid, Payment> paymentHistory = new ConcurrentDictionary<Guid, Payment>();
        private readonly IBankClient bank;

        public PaymentGateway(IBankClient bankClient)
        {
            bank = bankClient;
        }

        public async Task<Payment> FindPaymentAsync(Guid paymentId)
        {
            return await Task.Run(() => paymentHistory.TryGetValue(paymentId, out var payment) ? payment : null);
        }

        public async Task<Payment> PayAsync(PaymentDTO dto)
        {
            Payment payment = new Payment(dto);
            paymentHistory.Add(payment.Id, payment);
            var result = await bank.Pay(payment);

            var updatedPayment = result.Status == BankPaymentStatusOptions.Confirmed ? payment.Confirm(result.Id) : payment.Fail(result.Id);

            paymentHistory[payment.Id] = updatedPayment;
            paymentHistory.Add(result.Id, updatedPayment);
            return updatedPayment;
        }

    }
}
