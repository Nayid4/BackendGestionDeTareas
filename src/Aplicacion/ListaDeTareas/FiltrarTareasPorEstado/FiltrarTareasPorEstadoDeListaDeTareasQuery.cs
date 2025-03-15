
using Aplicacion.ListaDeTareas.Comun;

namespace Aplicacion.ListaDeTareas.FiltrarTareasPorEstado
{
    public record FiltrarTareasPorEstadoDeListaDeTareasQuery(Guid IdListaDeTareas, string EstadoTarea) : IRequest<ErrorOr<RespuestaListaDeTareas?>>;
}
