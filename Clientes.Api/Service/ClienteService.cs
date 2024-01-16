using Clientes.Api.Domain.Filters;
using Clientes.Api.Domain.Model;
using Clientes.Api.Infrastructure;

namespace Clientes.Api.Service
{
    public class ClienteService(AppDbContext appDbContext) : IClienteService
    {
        public readonly AppDbContext _appDbContext = appDbContext;

        public List<Cliente> ListarClientes(ClienteFiltros clienteFiltros) {
            var query = _appDbContext.Clientes.AsQueryable();
            if(!string.IsNullOrEmpty(clienteFiltros.NomeRazao))
            {
                query = query.Where(c => c.NomeRazao.Contains(clienteFiltros.NomeRazao));
            }

            if (!string.IsNullOrEmpty(clienteFiltros.Email))
            {
                query = query.Where(c => c.Email.Contains(clienteFiltros.Email));
            }

            if (!string.IsNullOrEmpty(clienteFiltros.Telefone))
            {
                query = query.Where(c => c.Telefone.Contains(clienteFiltros.Telefone));
            }

            if (clienteFiltros.Bloqueado != null)
            {
                query = query.Where(c => c.Bloqueado == clienteFiltros.Bloqueado);
            }

            return [.. query];
        }
    }
}
