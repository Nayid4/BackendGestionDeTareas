

namespace Aplicacion.Datos
{
    public interface IAplicacionContextoDb
    {

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
