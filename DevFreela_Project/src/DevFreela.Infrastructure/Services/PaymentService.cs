using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService _message;
        private readonly string QUEUE_NAME = "Payments";
        public PaymentService(IMessageBusService message)
        {
            _message = message;
        }
        public void ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            // Lógica de pagamento com gateway de pagamento;
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);
            
            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            _message.Publish(QUEUE_NAME, paymentInfoBytes);

        }
    }
}
