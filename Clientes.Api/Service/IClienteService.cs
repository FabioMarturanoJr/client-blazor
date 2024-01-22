using Clientes.Api.Domain.Dto;
using Clientes.Api.Domain.Filters;
using Clientes.Api.Domain.Model;

namespace Clientes.Api.Service;

public interface IClienteService
{
	CreateClienteDto BuscarPorId(int id);
	List<Cliente> ListarClientes(ClienteFiltros clienteFiltros);
    public void CadastraCliente(CreateClienteDto clienteDto);
    public void EditarCliente(CreateClienteDto Cliente, int ClienteId);
}
