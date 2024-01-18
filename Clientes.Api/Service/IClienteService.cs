using Clientes.Api.Domain.Dto;
using Clientes.Api.Domain.Filters;
using Clientes.Api.Domain.Model;

namespace Clientes.Api.Service;

public interface IClienteService
{
    List<Cliente> ListarClientes(ClienteFiltros clienteFiltros);
    bool ExisteEmail(string? email);
    bool ExisteCpfCnpj(string? cpfCnpj);
    bool ExisteInscricaoEstadual(string? inscricaoEstadual);
    public void CadastraCliente(CreateClienteDto clienteDto);
}
