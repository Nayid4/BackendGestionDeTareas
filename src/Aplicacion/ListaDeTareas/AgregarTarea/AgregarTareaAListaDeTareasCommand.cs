using Aplicacion.ListaDeTareas.Comun;

namespace Aplicacion.ListaDeTareas.AgregarTarea
{
    public record AgregarTareaAListaDeTareasCommand(Guid IdListaDeTareas, ComandoTarea Tarea) : IRequest<ErrorOr<Unit>>;
}
