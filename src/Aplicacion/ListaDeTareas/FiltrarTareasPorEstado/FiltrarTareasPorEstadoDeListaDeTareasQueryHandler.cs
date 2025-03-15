using Dominio.ListasDeTareas;
using Aplicacion.ListaDeTareas.Comun;

namespace Aplicacion.ListaDeTareas.FiltrarTareasPorEstado
{
    public class FiltrarTareasPorEstadoDeListaDeTareasQueryHandler : IRequestHandler<FiltrarTareasPorEstadoDeListaDeTareasQuery, ErrorOr<RespuestaListaDeTareas?>>
    {
        private readonly IRepositorioListaDeTareas _repositorioListaDeTareas;

        public FiltrarTareasPorEstadoDeListaDeTareasQueryHandler(IRepositorioListaDeTareas repositorioListaDeTareas)
        {
            _repositorioListaDeTareas = repositorioListaDeTareas ?? throw new ArgumentNullException(nameof(repositorioListaDeTareas));
        }

        public async Task<ErrorOr<RespuestaListaDeTareas?>> Handle(FiltrarTareasPorEstadoDeListaDeTareasQuery consulta, CancellationToken cancellationToken)
        {
            if (await _repositorioListaDeTareas.ListarPorId(new IdListaDeTareas(consulta.IdListaDeTareas)) is not ListaDeTarea lista)
            {
                return Error.NotFound("Lista.NoEncontrada", "No se encontró la lista de tareas.");
            }

            var tareasFiltradas = consulta.EstadoTarea.Equals("Todas") ? 
                lista.ListarTodasLasTareas() : 
                lista.FiltrarPorEstado(consulta.EstadoTarea);


            var listaFiltrada = new RespuestaListaDeTareas(
                lista.Id.Id, 
                lista.Titulo,
                tareasFiltradas.Select(t => new RespuestaTarea(
                    t.Id.Id,
                    t.Titulo,
                    t.Descripcion,
                    t.Estado
                )).ToList()
            );


            return listaFiltrada;
        }
    }
}
