using Clientes.Api.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Api.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes { get; set; }
}
