using Dominio.ListasDeTareas;
using Dominio.Tareas;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Datos
{
    public interface IAplicacionContextoDb
    {
        public DbSet<ListaDeTarea> ListaDeTareas { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
