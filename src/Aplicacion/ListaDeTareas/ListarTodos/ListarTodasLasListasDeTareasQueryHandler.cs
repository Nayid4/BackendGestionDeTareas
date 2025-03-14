using Aplicacion.ListaDeTareas.Comun;
using Dominio.ListasDeTareas;

namespace Aplicacion.ListaDeTareas.ListarTodos
{
    public class ListarTodasLasListasDeTareasQueryHandler : IRequestHandler<ListarTodasLasListasDeTareasQuery, ErrorOr<List<RespuestaListaDeTareas>>>
    {
        private readonly IRepositorioListaDeTareas _repositorioListaDeTareas;

        public ListarTodasLasListasDeTareasQueryHandler(IRepositorioListaDeTareas repositorioListaDeTareas)
        {
            _repositorioListaDeTareas = repositorioListaDeTareas;
        }

        public async Task<ErrorOr<List<RespuestaListaDeTareas>>> Handle(ListarTodasLasListasDeTareasQuery consulta, CancellationToken cancellationToken)
        {
            IReadOnlyList<ListaDeTarea> listas = await _repositorioListaDeTareas.ListarTodos();

            var lista = listas.Select(t => new RespuestaListaDeTareas(
                t.Id.Id,
                t.Titulo,
                t.Tareas.Select(ta => new RespuestaTarea(
                    ta.Id.Id,
                    ta.Titulo,
                    ta.Descripcion,
                    ta.Estado
                )).ToList()
                )).ToList();

            return lista;
        }
    }
}
