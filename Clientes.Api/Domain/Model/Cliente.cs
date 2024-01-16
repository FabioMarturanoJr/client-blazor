namespace Clientes.Api.Domain.Model;

public class Cliente
{
    public int Id { get; set; }
    public string? NomeRazao { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public DateTime DataCadastro { get; set; }
    public bool Bloqueado { get; set; }
}
