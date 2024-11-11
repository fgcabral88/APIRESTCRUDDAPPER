using APIRESTCRUDDAPPER.Dto;
using FluentValidation;

namespace APIRESTCRUDDAPPER.Application.Validations
{
    public class UsuarioCriarDtoValidator : AbstractValidator<UsuarioCriarDto>
    {
        public UsuarioCriarDtoValidator()
        {
            RuleFor(x => x.NomeCompleto)
                .NotNull()
                .NotEmpty()
                .WithMessage("O nome é obrigatório.")
                .Length(3, 100)
                .WithMessage("O nome deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("O e-mail é obrigatório.")
                .EmailAddress()
                .WithMessage("O e-mail deve ser válido.");

            RuleFor(x => x.Cargo)
                .NotNull()
                .NotEmpty()
                .WithMessage("O cargo é obrigatório.")
                .Length(3, 100)
                .WithMessage("O cargo deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.Salario)
                .NotNull()
                .NotEmpty()
                .WithMessage("O salário é obrigatório.")
                .GreaterThan(0)
                .WithMessage("O salário deve ser maior que zero.");

            RuleFor(x => x.CPF)
                .NotNull()
                .NotEmpty()
                .WithMessage("O CPF é obrigatório.")
                .Length(11)
                .WithName("CPF")
                .Matches("^[0-9]*$")
                .WithMessage("O CPF deve conter apenas números.");

            RuleFor(x => x.Senha)
                .NotNull()
                .NotEmpty()
                .WithMessage("A senha é obrigatória.")
                .Length(3,8)
                .WithMessage("A senha deve ter no mínimo 3 e máximo 8 caracteres.")
                .Matches("[A-Z]")
                .WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("A senha deve conter pelo menos um caractere especial.");
        }
    }
}
