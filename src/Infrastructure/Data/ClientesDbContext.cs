using ClientesCRM.src.Core.Entities;
using ClientesCRM.src.Infrastructure.Data.ConfigContext;
using Microsoft.EntityFrameworkCore;

namespace ClientesCRM.src.Infrastructure.Data
{
    public class ClientesDbContext : DbContext
    {
        public ClientesDbContext(DbContextOptions<ClientesDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }

        protected void OModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClienteConfigContext).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PropiedadConfigContext).Assembly);
        }
    }
}