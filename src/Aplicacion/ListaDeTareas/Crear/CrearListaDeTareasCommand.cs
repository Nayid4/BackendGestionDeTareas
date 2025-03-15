
namespace Aplicacion.ListaDeTareas.Crear
{
    public record CrearListaDeTareasCommand(string Titulo) : IRequest<ErrorOr<Unit>>;
}
