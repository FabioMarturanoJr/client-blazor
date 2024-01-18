using Clientes.Api.Domain.Enum;

namespace Clientes.Api.Domain.Model;

public class Cliente
{
    public int Id { get; set; }
    public string NomeRazao { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public TipoPessoaEnum TipoPessoa { get; set; }
    public string CpfCnpj { get; set; }
    public string? InscricaoEstadual { get; set; }
    public bool InscricaoEstadualPessoaFisica { get; set; }
    public bool Isento { get; set; }
    public GeneroEnum? Genero { get; set; }
    public DateTime? DataNascimento { get; set; }
    public bool Bloqueado { get; set; }
    public string Senha { get; set; }
    public DateTime DataCadastro { get; set; }
}
