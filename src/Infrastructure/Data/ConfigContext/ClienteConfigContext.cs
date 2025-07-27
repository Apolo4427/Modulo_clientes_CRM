using ClientesCRM.src.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientesCRM.src.Infrastructure.Data.ConfigContext
{
    public class ClienteConfigContext : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Cliente> e)
        {
            e.ToTable("Clientes");
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID()");
            e.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            e.Property(c => c.Apellido).IsRequired().HasMaxLength(100);
            e.Property(c => c.Telefono).HasMaxLength(20);
            e.Property(c => c.CorreoElectronico).HasMaxLength(200);
            e.Property(c => c.DireccionPrincipal).HasMaxLength(200);
            e.Property(c => c.NotasGenerales).HasMaxLength(1000);

            e.HasMany(c => c.Propiedades)
             .WithOne()
             .HasForeignKey(p => p.ClienteId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}