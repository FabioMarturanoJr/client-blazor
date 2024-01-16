using Clientes.Api.Domain.Model;
using Clientes.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController(AppDbContext appDbContext) : ControllerBase
    {
        public readonly AppDbContext _appDbContext = appDbContext;

        [HttpGet]
        public IList<Cliente> ListarClientes() => _appDbContext.Clientes.ToList();
    }
}
