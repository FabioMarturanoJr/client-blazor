namespace Clientes.Api.Domain.Filters;

public class ClienteFiltros
{
    public string? NomeRazao { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public DateTimeOffset? DataCadastroInicio { get; set; }
    public DateTimeOffset? DataCadastroFim { get; set; }
    public bool? Bloqueado { get; set; } = null;
}
