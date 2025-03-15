
namespace Aplicacion.ListaDeTareas.EliminarTarea
{
    public record EliminarTareaDeListaDeTareasCommand(Guid IdListaDeTareas, Guid IdTarea) : IRequest<ErrorOr<Unit>>;
}
