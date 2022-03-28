using DevFreela.Core.DTOs;
using DevFreela.Core.Services;

namespace DevFreela.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            // Lógica de pagamento com gateway de pagamento;

            return Task.FromResult(true);
        }
    }
}
