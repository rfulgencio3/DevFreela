using DevFreela.Core.Entities;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IAuthService _service;
        public CreateUserCommandHandler(
            DevFreelaDbContext dbContext,
            IAuthService service
            )
        {
            _dbContext = dbContext;
            _service = service;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _service.ComputeSha256Hash(request.Password);

            var user = new User(request.FullName, request.Email, request.BirthDate.Date, passwordHash, request.Role);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }
    }
}