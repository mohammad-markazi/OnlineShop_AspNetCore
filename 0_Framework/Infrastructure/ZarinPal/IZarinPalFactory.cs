using System.Threading.Tasks;

namespace _0_Framework.Infrastructure.ZarinPal
{
    public interface IZarinPalFactory
    {
        string Prefix { get; set; }

         Task<PaymentResponse> CreatePaymentRequest(string amount, string mobile, string email, string description,
            long orderId);

         Task<VerificationResponse> CreateVerificationRequest(string authority, string price);
    }
}