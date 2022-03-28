using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject
{

    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, bool>
    {
        private readonly IProjectRepository _repository;
        private readonly IPaymentService _service;
        public FinishProjectCommandHandler(
            IProjectRepository repository, 
            IPaymentService service)
        {
            _service = service;
            _repository = repository;
        }

        public async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetByIdAsync(request.Id);
            project.Finish();

            var paymentInfoDto = new PaymentInfoDTO(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.FullName);
            var result = await _service.ProcessPayment(paymentInfoDto);

            if (!result) project.SetPaymentPending();

            await _repository.SaveChangesAsync();
            return result;
        }
    }
}
