using ClientesCRM.src.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientesCRM.src.Infrastructure.Data.ConfigContext
{
    public class PropiedadConfigContext : IEntityTypeConfiguration<Propiedad>
    {
        public void Configure(EntityTypeBuilder<Propiedad> e)
        {
            e.ToTable("Propiedades");
            e.HasKey(p => p.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID()");
            e.Property(p => p.Calle).IsRequired().HasMaxLength(200);
            e.Property(p => p.Estado).IsRequired();
            e.Property(p => p.CodigoZip).IsRequired();

            e.HasOne<Cliente>()
             .WithMany(c => c.Propiedades)
             .HasForeignKey(p => p.ClienteId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}