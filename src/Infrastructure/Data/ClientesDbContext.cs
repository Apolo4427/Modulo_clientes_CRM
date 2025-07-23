using ClientesCRM.src.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientesCRM.src.Infrastructure.Data
{
    public class ClientesDbContext : DbContext
    {
        public ClientesDbContext(DbContextOptions<ClientesDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }

        // TODO: registrar configuraciones para la base de datos
    }
}