
namespace Aplicacion.ListaDeTareas.EliminarTarea
{
    public record EliminarTareaDeListaDeTareasCommand(Guid IdListaDeTarea, Guid IdTarea) : IRequest<ErrorOr<Unit>>;
}
