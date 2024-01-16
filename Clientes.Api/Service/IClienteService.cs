using Clientes.Api.Domain.Filters;
using Clientes.Api.Domain.Model;

namespace Clientes.Api.Service;

public interface IClienteService
{
    List<Cliente> ListarClientes(ClienteFiltros clienteFiltros);
}
