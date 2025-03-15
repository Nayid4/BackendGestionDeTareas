
namespace Aplicacion.ListaDeTareas.Eliminar
{
    public record EliminarListaDeTareasCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
