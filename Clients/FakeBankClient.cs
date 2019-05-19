using Model.Clients;
using System;
using System.Threading.Tasks;
using TamasBarabas_Checkout.Model;

namespace TamasBarabas_Checkout.Clients
{


    public class FakeBankClient : IBankClient
    {
        Random r = new Random();

        public async Task<BankPaymentResult> Pay(Payment payment)
        {
            return await Task.Run(async () => {
                await Task.Delay(TimeSpan.FromSeconds(1));
                return new BankPaymentResult(
                    r.NextDouble() >= 0.5 ? BankPaymentStatusOptions.Confirmed : BankPaymentStatusOptions.Failed,
                    Guid.NewGuid());
            });
        }
    }


    public class FakeBankAlwaysSucceedClient : IBankClient
    {
        public async Task<BankPaymentResult> Pay(Payment payment)
        {
            return await Task.Run(() => new BankPaymentResult(BankPaymentStatusOptions.Confirmed, Guid.NewGuid()));
        }
    }


    public class FakeBankAlwaysFailsClient : IBankClient
    {
        public async Task<BankPaymentResult> Pay(Payment payment)
        {
            return await Task.Run(() => new BankPaymentResult(BankPaymentStatusOptions.Failed, Guid.NewGuid()));
        }
    }


}
