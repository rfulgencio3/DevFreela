using DevFreela.Application.Commands.CreateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("Campo Título é obrigatório.");

            RuleFor(p => p.Title)
                .MaximumLength(30)
                .WithMessage("Tamanho maximo de Titulo é de 30 caracteres.");

            RuleFor(p => p.Description)
                .NotNull()
                .MaximumLength(255)
                .WithMessage("Tamanho máximo de Descrição é de 255 caracteres.");
        }
    }
}
