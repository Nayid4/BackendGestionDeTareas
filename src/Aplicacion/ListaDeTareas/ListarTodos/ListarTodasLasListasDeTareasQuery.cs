using Aplicacion.ListaDeTareas.Comun;

namespace Aplicacion.ListaDeTareas.ListarTodos
{
    public record ListarTodasLasListasDeTareasQuery() : IRequest<ErrorOr<List<RespuestaListaDeTareas>>>;
}
