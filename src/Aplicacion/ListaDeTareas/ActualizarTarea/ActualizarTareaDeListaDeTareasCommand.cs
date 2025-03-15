using Aplicacion.ListaDeTareas.Comun;

namespace Aplicacion.ListaDeTareas.ActualizarTarea
{
    public record ActualizarTareaDeListaDeTareasCommand(Guid IdListaDeTareas, RespuestaTarea Tarea) : IRequest<ErrorOr<Unit>>;
}
