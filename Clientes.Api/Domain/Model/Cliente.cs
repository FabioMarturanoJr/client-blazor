using Clientes.Api.Domain.Enum;

namespace Clientes.Api.Domain.Model;

public class Cliente
{
    public int Id { get; set; }
    public string NomeRazao { get; set; }
    public string Email { get; set; }// se existe
    public string Telefone { get; set; }
    public DateTime DataCadastro { get; set; }
    public TipoPessoa TipoPessoa { get; set; }
    public string CpfCnpj { get; set; }// se existe
    public string? InscricaoEstadual { get; set; }// se existe
    public bool? InscricaoEstadualPessoaFisica { get; set; }
    public bool? Isento { get; set; }
    public Genero? Genero { get; set; }
    public DateTime? DataNascimento { get; set; }
    public bool Bloqueado { get; set; }
    public string Senha { get; set; }
}
