using Clientes.Api.Domain.Enum;
using Clientes.Api.Domain.Model;
using Clientes.Api.Extensions;
using FluentValidation;

namespace Clientes.Api.Domain.Dto;

public class CreateClienteDto
{
    public string NomeRazao { get; set; }
    public string Email { get; set; }// se existe
    public string Telefone { get; set; }
    public string TipoPessoa { get; set; }
    public string? Cpf { get; set; }// se existe
    public string? Cnpj { get; set; }// se existe
    public string? InscricaoEstadual { get; set; }// se existe
    public bool InscricaoEstadualPessoaFisica { get; set; }
    public bool Isento { get; set; }
    public string? Genero { get; set; }
    public DateTime? DataNascimento { get; set; }
    public bool Bloqueado { get; set; }
    public string Senha { get; set; }
    public string ConfirmaSenha { get; set; }

    public static explicit operator Cliente(CreateClienteDto dto) =>
        new()
        {
            NomeRazao = dto.NomeRazao,
            Email = dto.Email,
            Telefone = dto.Telefone.RemoveCaracterEspecial()!,
            TipoPessoa = dto.TipoPessoa.ConvertEnum<TipoPessoaEnum>(),
            CpfCnpj = dto.TipoPessoa == TipoPessoaEnum.Fisica.ToString("g") ? dto.Cpf.RemoveCaracterEspecial()! : dto.Cnpj.RemoveCaracterEspecial()!,
            InscricaoEstadual = dto.InscricaoEstadual.RemoveCaracterEspecial(),
            InscricaoEstadualPessoaFisica  = dto.InscricaoEstadualPessoaFisica,
            Isento = dto.Isento,
            Genero = dto.Genero?.ConvertEnum<GeneroEnum>(),
            DataNascimento = dto.DataNascimento,
            Bloqueado = dto.Bloqueado,
            Senha = dto.Senha,
            DataCadastro = DateTime.Now,
        };
}


public class CadastroClienteValidations : AbstractValidator<CreateClienteDto>
{
    public CadastroClienteValidations()
    {
        RuleFor(x => x.NomeRazao).NotEmpty().WithMessage("NomeRazao Obrigatorio").MaximumLength(150);

        RuleFor(x => x.Email).NotEmpty().WithMessage("Email Obrigatorio")
            .Must(x => x.EmailValido()).WithMessage("Email deve ser válido")
            .MaximumLength(150);

        RuleFor(x => x.Telefone).NotEmpty().WithMessage("Telefone Obrigatorio")
            .Must(x => x.RemoveCaracterEspecial()?.Length == 11).WithMessage("Telefone deve conter 11 caracteres");

        RuleFor(x => x.TipoPessoa).NotEmpty().WithMessage("TipoPessoa Obrigatorio");

        RuleFor(x => x.Cpf).NotEmpty().WithMessage("Cpf Obrigatorio")
            .Must(x => x.RemoveCaracterEspecial()?.Length == 11).WithMessage("Cpf deve conter 11 caracteres")
            .When(x => x.Cpf != null || x.TipoPessoa == TipoPessoaEnum.Fisica.ToString("g"));

        RuleFor(x => x.Cnpj).NotEmpty().WithMessage("Cnpj Obrigatorio")
            .Must(x => x.RemoveCaracterEspecial()?.Length == 14).WithMessage("Cnpj deve conter 14 caracteres")
            .When(x => x.Cnpj != null || x.TipoPessoa == TipoPessoaEnum.Juridica.ToString("g"));

        RuleFor(x => x.InscricaoEstadual).NotEmpty().WithMessage("InscricaoEstadual Obrigatorio")
            .Must(x => x.RemoveCaracterEspecial()?.Length == 12).WithMessage("InscricaoEstadual deve conter 12 caracteres")
            .When(x => !x.Isento && (x.TipoPessoa == TipoPessoaEnum.Juridica.ToString("g") || x.InscricaoEstadualPessoaFisica));

        RuleFor(x => x.Genero).NotEmpty().WithMessage("Genero Obrigatorio")
            .When(x => x.TipoPessoa == TipoPessoaEnum.Fisica.ToString("g"));

        RuleFor(x => x.DataNascimento).NotEmpty().WithMessage("DataNascimento Obrigatorio")
            .When(x => x.TipoPessoa == TipoPessoaEnum.Fisica.ToString("g"));

        RuleFor(x => x.Senha).NotEmpty().MinimumLength(8).MaximumLength(15);

        RuleFor(x => x.ConfirmaSenha).NotEmpty().MinimumLength(8).MaximumLength(15).Equal(x => x.Senha).WithMessage("Senhas estão diferentes");
    }
}
