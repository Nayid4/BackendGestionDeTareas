
namespace Aplicacion.ListaDeTareas.EliminarTarea
{
    public record EliminarTareaDeListaDeTareasCommand(Guid Id, Guid IdTarea) : IRequest<ErrorOr<Unit>>;
}
