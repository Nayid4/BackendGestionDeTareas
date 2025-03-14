
namespace Aplicacion.ListaDeTareas.ActualizarEstadoDeTarea
{
    public record ActualizarEstadoDeTareaDeListaDeTareasCommand(Guid IdListaDeTareas, Guid IdTarea, string Estado) : IRequest<ErrorOr<Unit>>;
}
