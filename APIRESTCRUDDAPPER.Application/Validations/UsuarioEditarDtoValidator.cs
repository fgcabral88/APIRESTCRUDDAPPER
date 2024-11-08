﻿using APIRESTCRUDDAPPER.Dto;
using FluentValidation;

namespace APIRESTCRUDDAPPER.Application.Validations
{
    public class UsuarioEditarDtoValidator : AbstractValidator<UsuarioEditarDto>
    {
        public UsuarioEditarDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("O id é obrigatório.");

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
                .WithMessage("O cargo é obrigatório.");

            RuleFor(x => x.Salario)
                .NotNull()
                .NotEmpty()
                .WithMessage("O salário é obrigatório.");

            RuleFor(x => x.CPF)
                .NotNull()
                .NotEmpty()
                .WithMessage("O CPF é obrigatório.")
                .Length(11)
                .WithName("CPF");
        }
    }
}
