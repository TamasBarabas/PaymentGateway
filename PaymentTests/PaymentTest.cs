using Model.Clients;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Threading.Tasks;
using TamasBarabas_Checkout.Clients;
using TamasBarabas_Checkout.Model;

namespace PaymentTests
{
    class PaymentGatewayTest
    {
        PaymentDTO TestPayment = new PaymentDTO()
        {
            CardNumber = "1234-1234-1234-1234",
            ExpiryMonth = 11,
            ExpiryYear = 2919,
            AmountInCents = 123,
            Currency = "GBP",
            Cvv = 123
        };

        [Test]
        public async Task CallingPayAsyncOnSuccessfulFakeBank_ShouldReturnDtosPaymentData()
        {

            IBankClient bank = new FakeBankAlwaysSucceedClient();
            PaymentGateway pg = new PaymentGateway(bank);

            var payment = await pg.PayAsync(TestPayment);
            Assert.AreEqual(TestPayment.CardNumber, payment.CardNumber);
            Assert.AreEqual(TestPayment.ExpiryMonth, payment.ExpiryMonth);
            Assert.AreEqual(TestPayment.ExpiryYear, payment.ExpiryYear);
            Assert.AreEqual(TestPayment.AmountInCents, payment.AmountInCents);
            Assert.AreEqual(TestPayment.Currency, payment.Currency);
            Assert.AreEqual(TestPayment.Cvv, payment.Cvv);
        }

        [Test]
        public async Task CallingPayAsyncOnSuccessfulFakeBank_ShouldReturnSuccessfulPayment()
        {

            IBankClient bank = new FakeBankAlwaysSucceedClient();
            PaymentGateway pg = new PaymentGateway(bank);

            var payment = await pg.PayAsync(TestPayment);
            Assert.AreEqual(PaymentStatusOptions.Confirmed, payment.PaymentStatus);
        }

        [Test]
        public async Task CallingFindPaymentAsyncOnSuccessfulFakeBank_ShouldReturnPreviousPayment()
        {

            IBankClient bank = new FakeBankAlwaysSucceedClient();
            PaymentGateway pg = new PaymentGateway(bank);

            var payment = await pg.PayAsync(TestPayment);
            var previousPayment = await pg.FindPaymentAsync(payment.Id);

            Assert.AreEqual(payment, previousPayment);
        }


        [Test]
        public async Task CallingFindPaymentAsyncOnSuccessfulFakeBank_ShouldReturnPreviousPaymentWithBankId()
        {

            IBankClient bank = new FakeBankAlwaysSucceedClient();
            PaymentGateway pg = new PaymentGateway(bank);

            var payment = await pg.PayAsync(TestPayment);
            var previousPayment = await pg.FindPaymentAsync(payment.BankId);

            Assert.AreEqual(payment, previousPayment);
        }


        [Test]
        public async Task TwoDifferentPayment_ShouldNotBeEaual()
        {

            IBankClient bank = new FakeBankAlwaysSucceedClient();
            PaymentGateway pg = new PaymentGateway(bank);

            var payment = await pg.PayAsync(TestPayment);
            var payment2 = await pg.PayAsync(TestPayment);
            var previousPayment = await pg.FindPaymentAsync(payment.Id);

            Assert.AreNotEqual(payment2, previousPayment);
        }

        [Test]
        public async Task CallingPayAsyncOnUnsuccessfulFakeBank_ShouldReturnDtosPaymentData()
        {

            IBankClient bank = new FakeBankAlwaysFailsClient();
            PaymentGateway pg = new PaymentGateway(bank);

            var payment = await pg.PayAsync(TestPayment);
            Assert.AreEqual(TestPayment.CardNumber, payment.CardNumber);
            Assert.AreEqual(TestPayment.ExpiryMonth, payment.ExpiryMonth);
            Assert.AreEqual(TestPayment.ExpiryYear, payment.ExpiryYear);
            Assert.AreEqual(TestPayment.AmountInCents, payment.AmountInCents);
            Assert.AreEqual(TestPayment.Currency, payment.Currency);
            Assert.AreEqual(TestPayment.Cvv, payment.Cvv);
        }

        [Test]
        public async Task CallingPayAsyncOnUnsuccessfulFakeBank_ShouldReturnSuccessfulPayment()
        {

            IBankClient bank = new FakeBankAlwaysFailsClient();
            PaymentGateway pg = new PaymentGateway(bank);

            var payment = await pg.PayAsync(TestPayment);
            Assert.AreEqual(PaymentStatusOptions.Failed, payment.PaymentStatus);
        }

        [Test]
        public async Task CallingFindPaymentAsyncOnUnsuccessfulFakeBank_ShouldReturnPreviousPayment()
        {

            IBankClient bank = new FakeBankAlwaysSucceedClient();
            PaymentGateway pg = new PaymentGateway(bank);

            var payment = await pg.PayAsync(TestPayment);
            var payment2 = await pg.PayAsync(TestPayment);
            var previousPayment = await pg.FindPaymentAsync(payment.Id);

            Assert.AreEqual(payment, previousPayment);
        }




    }
}
