using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IAuthService _service;
        private readonly IUserRepository _repository;

        public LoginUserCommandHandler(
            IAuthService service, 
            IUserRepository repository)
        {
            _service = service;
            _repository = repository;
        }
        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Utilizar o mesmo algoritimo para criar o hash da senha
            var passwordHash = _service.ComputeSha256Hash(request.Password);

            // Buscar no BD um User que tenha o e-mail e senha em formato hash conforme requisitado
            var user = await _repository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);

            // Se não existir, erro no login
            if (user == null) return null;

            // Se existir, gerar o token com os dados do usuário
            var token = _service.GenerateJwtToken(user.Email, user.Role);

            return new LoginUserViewModel(user.Email, token);
        }
    }
}
