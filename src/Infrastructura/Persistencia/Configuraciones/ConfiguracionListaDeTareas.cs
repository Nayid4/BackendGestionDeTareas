using Dominio.ListasDeTareas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistencia.Configuraciones
{
    public class ConfiguracionListaDetareas : IEntityTypeConfiguration<ListaDeTarea>
    {
        public void Configure(EntityTypeBuilder<ListaDeTarea> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                inst => inst.Id,
                valor => new IdListaDeTareas(valor));

            builder.Property(t => t.Titulo)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(t => t.Tareas)
                .WithOne()
                .HasForeignKey(ta => ta.IdListaDetareas);

            builder.Property(t => t.FechaDeCreacion);
        }
    }
}
