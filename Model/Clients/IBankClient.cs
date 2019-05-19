using System.Threading.Tasks;
using TamasBarabas_Checkout.Model;

namespace Model.Clients
{
    public interface IBankClient
    {
        Task<BankPaymentResult> Pay(Payment payment);
    }
}
