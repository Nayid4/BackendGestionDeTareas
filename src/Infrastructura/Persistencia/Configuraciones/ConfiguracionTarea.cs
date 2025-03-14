using Dominio.ListasDeTareas;
using Dominio.Tareas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructura.Persistencia.Configuraciones
{
    public class ConfiguracionTarea : IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                inst => inst.Id,
                valor => new IdTarea(valor));

            builder.Property(t => t.IdListaDetareas).HasConversion(
                inst => inst.Id,
                valor => new IdListaDeTareas(valor));

            builder.Property(t => t.Titulo)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Descripcion)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(t => t.Estado)
                .HasMaxLength(25)
                .IsRequired();

        }
    }
}
